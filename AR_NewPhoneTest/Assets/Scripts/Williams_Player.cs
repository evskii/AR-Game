using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Williams_Player : MonoBehaviour {

    public static Williams_Player instance;

    private void Awake() {
        instance = this;
    }

    private NavMeshAgent myAgent;
    private Animator anim;

    public Camera cam;

    public bool hasAxe = false;

    public GameObject locationMarker;
    public GameObject currentLocationMarker;

    //Resources
    public int wood = 0;

    private void Start() {
        myAgent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();

        GameObject arCamera = GameObject.Find("ARCamera");
        cam = arCamera.GetComponent<Camera>();

    }

    

    private void Update() {

        //Change the player animation state when moving
        if (myAgent.velocity.magnitude > 0f) {
            anim.SetBool("Walking", true);
            if (myAgent.remainingDistance < 0.05f) {
                if (currentLocationMarker != null) {
                    currentLocationMarker.GetComponent<Williams_LocationMarker>().DestroyMe();
                }
            }
        } else {
            anim.SetBool("Walking", false);
        }

        if (EventSystem.current.IsPointerOverGameObject(0)) {
            return;
        }

        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit)) {
                if(hit.transform.tag == "Ground") {
                    if (currentLocationMarker != null) {
                        currentLocationMarker.GetComponent<Williams_LocationMarker>().DestroyMe();
                    }

                    currentLocationMarker = Instantiate(locationMarker, hit.point, locationMarker.transform.rotation);
                    GoToPoint(hit.point);

                }
            }
        }
    }

    public void GoToPoint(Vector3 point) {
        //Turn to look at destination
        transform.LookAt(point);
        //Start going
        myAgent.SetDestination(point);
    }


}
