using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Williams_CampFire : MonoBehaviour
{
    private Camera cam; //Camera used for raycasting

    private bool playerWalking = false; //Used when the player is walking towards the object tapped on

    public GameObject standPoint; //An empty where the player walks to and stands when interacting

    public GameObject canvas;
    public Button lightButton;
    public Text qty;

    public GameObject particleEffect;

    private GameObject player;


    private void Start() {
        GameObject arCamera = GameObject.Find("ARCamera"); //Find the ARCamera in the scene
        cam = arCamera.GetComponent<Camera>(); //and reference our camera to it

        displaySpeechbubble(false);
    }

    private void Update() {
        if (EventSystem.current.IsPointerOverGameObject(0)) {
            return;
        }

        //Used on PC for debugging
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit)) {
                if (hit.transform.gameObject == gameObject) {
                    displaySpeechbubble(!canvas.activeSelf);
                }
            }
        }

        //When the player is walking towards the tree
        if (playerWalking) {
            if (Vector3.Distance(standPoint.transform.position, player.transform.position) <= 0.3f) { //If the player is close to or ontop of the standpoint
                StartCoroutine(LightFire());
                playerWalking = false;
            }
        }
    }

    public void BeginMakingFire() {

        WalkToFire();
        displaySpeechbubble(false);
        Williams_Player.instance.wood--;

    }

    IEnumerator LightFire() {
        
        player.transform.LookAt(transform.position);
        player.GetComponentInChildren<Animator>().SetTrigger("Kneel");

        yield return new WaitForSeconds(2.5f);

        particleEffect.SetActive(true);

        GameManager.instance.trinketsCollected++;
        GameManager.instance.DisplayTrinketText();

        this.enabled = false;
    }

    public void WalkToFire() {
        player = GameObject.FindGameObjectWithTag("Player");
        Williams_Player.instance.GoToPoint(standPoint.transform.position);
        playerWalking = true;
    }

    private bool CheckMaterials() {
        if (Williams_Player.instance.wood >= 1) {
            return true;
        } else {
            return false;
        }
    }

    private void displaySpeechbubble(bool display) {
        canvas.SetActive(display);
        if (display) {
            qty.text = Williams_Player.instance.wood + " / 1";
            if (CheckMaterials()) {
                lightButton.interactable = true;
            } else {
                lightButton.interactable = false;
            }
        }

    }
}
