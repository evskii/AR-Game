﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class Williams_CabinMan : MonoBehaviour
{
    public static Williams_CabinMan instance;

    private void Awake() {
        instance = this;
    }


    private Camera cam; //Camera used for raycasting

    public GameObject speechBubble;
    public Text quantity;
    public Button craftButton;
    private bool uiEnabled = false;

    public GameObject cabinMan;

    bool inside;

    private NavMeshAgent agent;
    public Transform insideTransform;
    public Transform outsideTransform;
    private Animator animController;

    public GameObject door;



    private void Start() {

        GameObject arCamera = GameObject.Find("ARCamera"); //Find the ARCamera in the scene
        cam = arCamera.GetComponent<Camera>(); //and reference our camera to it

        displaySpeechbubble(false);

        animController = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        inside = true;

    }

    private void Update() {

        if (EventSystem.current.IsPointerOverGameObject(0)) {
            return;
        }

        if (!inside) {
            if (Input.GetMouseButtonDown(0)) {
                RaycastHit hit;
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit)) {
                    if (hit.transform.gameObject == gameObject) {
                        uiEnabled = !uiEnabled;
                        displaySpeechbubble(uiEnabled);

                    }
                }
            }
        }
        


        //Change the cabinman animation if moving or not
        if (agent.velocity.magnitude > 0f) {
            animController.SetBool("walking", true);
        } else {
            animController.SetBool("walking", false);
        }

        if (Input.GetKeyDown(KeyCode.O)) {
            GoOutside();
        }
        if (Input.GetKeyDown(KeyCode.I)) {
            GoInside();
        }

        if (playerWalking) {
            if (Vector3.Distance(standPoint.transform.position, player.transform.position) <= 0.3f) { //If the player is close to or ontop of the standpoint
                HandOverWood();
                playerWalking = false;
                Debug.Log("Hand it over");
            }
        }

    }

    public GameObject standPoint;
    public bool playerWalking = false;
    public GameObject player;

    public void GiveWood() {
        player = GameObject.FindGameObjectWithTag("Player");
        Williams_Player.instance.GoToPoint(standPoint.transform.position);
        playerWalking = true;

    }

    public void HandOverWood() {
        Williams_Player.instance.wood -= 2;
        displaySpeechbubble(false);
        SceneManager.LoadScene("WinScene");
    }


    private bool CheckMaterials() {
        if (Williams_Player.instance.wood >= 2) {
            return true;
        } else {
            return false;
        }
    }

    private void displaySpeechbubble(bool display) {
        speechBubble.SetActive(display);
        if (display) {
            quantity.text = Williams_Player.instance.wood + " / 2";
            if (CheckMaterials()) {
                craftButton.interactable = true;
            } else {
                craftButton.interactable = false;
            }
        }
    }

    

    public void GoInside() {
        Debug.Log("Inside");
        agent.enabled = true;
        animController.SetBool("walking", true);
        transform.LookAt(insideTransform.position);
        agent.GetComponent<NavMeshAgent>().SetDestination(insideTransform.position);
        inside = true;
        StartCoroutine(WaitToClose());
    }

    public void GoOutside() {
        door.GetComponent<Animator>().SetTrigger("Open");
        agent.enabled = true;
        animController.SetBool("walking", true);
        transform.LookAt(outsideTransform.position);
        agent.GetComponent<NavMeshAgent>().SetDestination(outsideTransform.position);
        Debug.Log("OUtside");
        inside = false;
    }

    IEnumerator WaitToClose() {
        yield return new WaitForSeconds(1.5f);
        door.GetComponent<Animator>().SetTrigger("Close");
    }

}
