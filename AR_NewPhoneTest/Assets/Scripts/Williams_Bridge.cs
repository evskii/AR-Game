using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Williams_Bridge : MonoBehaviour
{
    public GameObject bridgeConstructed;
    public GameObject bridgeBroken;

    public bool constructed = false;

    private Camera cam; //Camera used for raycasting

    public GameObject speechBubble;
    public Text quantity;
    public Button craftButton;
    private bool uiEnabled = false;



    private void Start() {

        constructed = false;

        bridgeConstructed.SetActive(false);
        bridgeBroken.SetActive(true);

        GameObject arCamera = GameObject.Find("ARCamera"); //Find the ARCamera in the scene
        cam = arCamera.GetComponent<Camera>(); //and reference our camera to it

        displaySpeechbubble(false);

    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Williams_Player.instance.wood++;
        }

        if (EventSystem.current.IsPointerOverGameObject(0)) {
            return;
        }

        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit)) {
                if (hit.transform.gameObject == gameObject) {
                    if (!constructed) {
                        uiEnabled = !uiEnabled;
                        displaySpeechbubble(uiEnabled);
                    }
                    
                }
            }
        }

        
    }


    public void BuildBridge() {
        Debug.Log("Build Bridge");
        constructed = true;


        bridgeBroken.SetActive(false);
        bridgeConstructed.SetActive(true);

        NavMeshReset.instance.ImmediateRebuildMesh();

        displaySpeechbubble(false);

        Williams_Player.instance.wood--;
    }

    private bool CheckMaterials() {
        if(Williams_Player.instance.wood >= 1) {
            return true;
        }else {
            return false;
        }
    }

    private void displaySpeechbubble(bool display) {
        speechBubble.SetActive(display);
        if (display) {
            quantity.text = Williams_Player.instance.wood + " / 1";
            if (CheckMaterials()) {
                craftButton.interactable = true;
            }else {
                craftButton.interactable = false;
            }
        }
        
    }
}
