using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Williams_ChoppingArea : MonoBehaviour
{
    private Camera cam; //Camera used for raycasting

    private bool playerWalking = false; //Used when the player is walking towards the object tapped on

    public GameObject player;
    public GameObject standPoint; //An empty where the player walks to and stands when interacting

    public GameObject exclamationMark;

    public GameObject bin;



    private void Start() {
        GameObject arCamera = GameObject.Find("ARCamera"); //Find the ARCamera in the scene
        cam = arCamera.GetComponent<Camera>(); //and reference our camera to it
        exclamationMark.SetActive(false);
    }

    private void Update() {
        //Used on PC for debugging
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit)) {
                if (hit.transform.gameObject == gameObject) {
                    WalkToBin();
                    exclamationMark.SetActive(true);

                }
            }
        }

        //When the player is walking towards the tree
        if (playerWalking) {
            if (Vector3.Distance(standPoint.transform.position, player.transform.position) <= 0.3f) { //If the player is close to or ontop of the standpoint
                OpenBin();
                playerWalking = false;
            }
        }
    }

    //Sets the destination for the palyer to walk to
    private void WalkToBin() {
        player = GameObject.FindGameObjectWithTag("Player");
        Williams_Player.instance.GoToPoint(standPoint.transform.position);
        playerWalking = true;
    }

    private void OpenBin() { //When the player makes it to the standpoint (no animations as of now)
        player.transform.LookAt(bin.transform.position);
        bin.GetComponent<Animator>().SetTrigger("Open");
        GameManager.instance.trinketsCollected++;
        StartCoroutine(DisplayText());
    }

    IEnumerator DisplayText() {
        yield return new WaitForSeconds(1f);
        GameManager.instance.DisplayTrinketText();
        exclamationMark.SetActive(false);
        Destroy(this);
    }
}
