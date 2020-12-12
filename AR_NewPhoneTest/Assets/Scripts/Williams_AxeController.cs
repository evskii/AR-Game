using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Williams_AxeController : MonoBehaviour
{
    private Camera cam; //Camera used for raycasting

    private bool playerWalking = false;

    public GameObject player;
    public GameObject standPoint;
    public GameObject exclamationMark;

    private void Start() {
        GameObject arCamera = GameObject.Find("ARCamera"); //Find the ARCamera in the scene
        cam = arCamera.GetComponent<Camera>(); //and reference our camera to it
        exclamationMark.SetActive(false);
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit)) {
                if (hit.transform.tag == "Axe") {
                    Debug.Log("AXE TIME");
                    player = GameObject.FindGameObjectWithTag("Player");
                    Williams_Player.instance.GoToPoint(standPoint.transform.position);
                    playerWalking = true;
                    exclamationMark.SetActive(true);
                }
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
