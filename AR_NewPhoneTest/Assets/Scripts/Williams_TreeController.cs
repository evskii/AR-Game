using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Williams_TreeController : MonoBehaviour
{
    private Camera cam; //Camera used for raycasting

    private bool playerWalking = false; //Used when the player is walking towards the object tapped on

    public GameObject player;
    public GameObject standPoint; //An empty where the player walks to and stands when interacting

    public GameObject exclamationMark;

    public GameObject stump;

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
                    Debug.Log("Tree TIME");
                    WalkToTree();
                    exclamationMark.SetActive(true);
                }
            }
        }

        //When the player is walking towards the tree
        if (playerWalking) {
            if (Vector3.Distance(standPoint.transform.position, player.transform.position) <= 0.3f) { //If the player is close to or ontop of the standpoint
                if (Williams_Player.instance.hasAxe) {
                    BeginChopping();
                }
                playerWalking = false;
            }
        }
    }

    //Sets the destination for the palyer to walk to
    private void WalkToTree() {
        player = GameObject.FindGameObjectWithTag("Player");
        Williams_Player.instance.GoToPoint(standPoint.transform.position);
        playerWalking = true;
    }

    private void BeginChopping() { //When the player makes it to the standpoint (no animations as of now)
        player.transform.LookAt(transform.position);
        player.GetComponentInChildren<Animator>().SetBool("SwingingAxe", true);
        player.GetComponentInChildren<Animator>().SetTrigger("StartSwinging");
        StartCoroutine(ChopTree());
    }

    IEnumerator ChopTree() { //Delay for chopping tree
        yield return new WaitForSeconds(2);
        Williams_Player.instance.wood++;
        Instantiate(stump, transform.position, transform.rotation);
        player.GetComponentInChildren<Animator>().SetBool("SwingingAxe", false);
        Destroy(gameObject); //Replace with stump in future rather than destroying completely
        
    }
}
