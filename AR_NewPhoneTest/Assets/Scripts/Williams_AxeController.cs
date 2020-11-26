using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Williams_AxeController : MonoBehaviour
{
    private Camera cam; //Camera used for raycasting

    private bool playerWalking = false;

    public GameObject player;
    public GameObject standPoint;

    private void Start() {
        GameObject arCamera = GameObject.Find("ARCamera"); //Find the ARCamera in the scene
        cam = arCamera.GetComponent<Camera>(); //and reference our camera to it

    }

    void CheckTouch() {
        RaycastHit hit;
        Touch touch = Input.GetTouch(0);
        Ray ray = cam.ScreenPointToRay(touch.position); //Raycast from where the player touched on the screen to the world

        if (Physics.Raycast(ray, out hit)) { //If our raycast makes contact
            if (hit.transform.tag == "Axe") {
                player = GameObject.FindGameObjectWithTag("Player");
                Williams_Player.instance.GoToPoint(standPoint.transform.position);
                playerWalking = true;
            }
        }
    }

    private void Update() {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit)) {
                if (hit.transform.tag == "Axe") {
                    Debug.Log("AXE TIME");
                    player = GameObject.FindGameObjectWithTag("Player");
                    Williams_Player.instance.GoToPoint(standPoint.transform.position);
                    playerWalking = true;
                }
            }
        }

#endif

        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began) {
                CheckTouch(); //call this every time the screen is touched (maybe not the most optimized)
            }
        }

        if (playerWalking) {
            if(Vector3.Distance(standPoint.transform.position, player.transform.position) < 0.1f) {
                Williams_Player.instance.hasAxe = true;
                Destroy(gameObject);
            }
        }
    }
}
