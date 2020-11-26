using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Williams_Player : MonoBehaviour {

    public static Williams_Player instance;

    private void Awake() {
        instance = this;
    }

    private NavMeshAgent myAgent;
    private Animator anim;

    public Camera cam;

    private bool active;
    public GameObject activeIndicator;

    public bool hasAxe = false;

    private void Start() {
        myAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        active = false;
        activeIndicator.SetActive(false);

        GameObject arCamera = GameObject.Find("ARCamera");
        cam = arCamera.GetComponent<Camera>();

    }

    

    private void Update() {

        CheckActive();

        if (active) {
            activeIndicator.SetActive(true);

        #if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0)) {
                RaycastHit hit;
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit)) {
                    if(hit.transform.tag == "Ground") {
                        GoToPoint(hit.point);
                    }
                }
            }

        #endif

        #if UNITY_ANDROID
            if (Input.touchCount > 0) {
                RaycastHit hit;
                Touch touch = Input.GetTouch(0);
                Ray ray = cam.ScreenPointToRay(touch.position);

                if (Physics.Raycast(ray, out hit)) {
                    if (hit.transform.tag == "Ground") {
                        GoToPoint(hit.point);
                    }
                }
            }
        #endif
            //Change the player animation state when moving
            if (Mathf.Abs(myAgent.velocity.x) > 0.1 || Mathf.Abs(myAgent.velocity.y) > 0.1) {
                anim.SetBool("Walking", true);
            }
            else {
                anim.SetBool("Walking", false);
            }
        }
        else {//Not Active
            activeIndicator.SetActive(false);
        }
    }

    //private NavMeshPath path;
    //public bool CanIReach(Vector3 point) {
    //    path = new NavMeshPath();
    //    int areaMask = NavMesh.GetAreaFromName("Not Walkable");
    //    if(NavMesh.CalculatePath(transform.position, point, areaMask, path)) {
    //        Debug.Log("Path Accessable");
    //        return true;
    //    }
    //    else {
    //        Debug.Log("Path no no");
    //        return false;
    //    }
        
    //}

    public void GoToPoint(Vector3 point) {
        //Turn to look at destination
        transform.LookAt(point);
        //Start going
        myAgent.SetDestination(point);
        
    }

    private void CheckActive() {
#if UNITY_EDITOR
        Debug.Log("Editor");
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit)) {
                if(hit.transform.tag == "Player") {
                    active = !active;
                }
            }
        }

#endif

#if UNITY_ANDROID
        Debug.Log("Android");
        if (Input.touchCount > 0) {
            RaycastHit hit;
            Touch touch = Input.GetTouch(0);
            Ray ray = cam.ScreenPointToRay(touch.position);
            if (touch.phase == TouchPhase.Began) {
                if (Physics.Raycast(ray, out hit)) {
                    if (hit.transform.tag == "Player") {
                        active = !active;
                    }
                }
            }
            
        }
#endif
    }

}
