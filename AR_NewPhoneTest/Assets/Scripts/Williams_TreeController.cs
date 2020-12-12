using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Williams_TreeController : MonoBehaviour
{
    private Camera cam; //Camera used for raycasting

    private bool playerWalking = false; //Used when the player is walking towards the object tapped on

    public GameObject player;
    public GameObject standPoint; //An empty where the player walks to and stands when interacting

    private void Start() {
        GameObject arCamera = GameObject.Find("ARCamera"); //Find the ARCamera in the scene
        cam = arCamera.GetComponent<Camera>(); //and reference our camera to it

    }

    void CheckTouch() {
        RaycastHit hit;
        Touch touch = Input.GetTouch(0);
        Ray ray = cam.ScreenPointToRay(touch.position); //Raycast from where the player touched on the screen to the world

        if (Physics.Raycast(ray, out hit)) { //If our raycast makes contact
            if (hit.transform.gameObject == gameObject) {
                WalkToTree();
            }
        }
    }

    private void Update() {
        //Used on PC for debugging
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit)) {
                if (hit.transform.gameObject == gameObject) {
                    Debug.Log("Tree TIME");
                    WalkToTree();
                   
                }
            }
        }

#endif
        //Touch controls (Check if there is an input and then start raycasting)
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began) {
                CheckTouch(); //call this every time the screen is touched (maybe not the most optimized)
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
        StartCoroutine(ChopTree());
    }

    IEnumerator ChopTree() { //Delay for chopping tree
        yield return new WaitForSeconds(2);
        Williams_Player.instance.wood++;
        Destroy(gameObject); //Replace with stump in future rather than destroying completely
    }
}
