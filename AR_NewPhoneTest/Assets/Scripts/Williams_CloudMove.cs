using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Williams_CloudMove : MonoBehaviour
{
    /* Script used to move the cloud when the player 
     * touches on it, and also whatever interactions
     * follow 
     */

    private Camera cam; //Camera used for raycasting

    private void Start() {

        GameObject arCamera = GameObject.Find("ARCamera"); //Find the ARCamera in the scene
        cam = arCamera.GetComponent<Camera>(); //and reference our camera to it

        startHeight = transform.position.y;
        destinationHeight = startHeight + animHeight;

    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit)) { //If our raycast makes contact
                if (hit.transform.tag == "Cloud") {
                    //Rain
                }
            }
        }

        AnimateCloud();
    }

    //Animate
    public float animHeight;
    public float lerpAmt;
    public float stoppingDistance;
    private float currentHeight;
    private float startHeight;

    private float destinationHeight;

    public void AnimateCloud() {
        currentHeight = transform.position.y;
        float yPos = Mathf.Lerp(currentHeight, destinationHeight, lerpAmt);
        Vector3 pos = new Vector3(transform.position.x, yPos, transform.position.z);
        transform.position = pos;

        if (Mathf.Abs((transform.position.y - destinationHeight)) < stoppingDistance) {
            if (destinationHeight > startHeight) {
                destinationHeight -= animHeight * 2;
            } else {
                destinationHeight += animHeight * 2;
            }

        }
    }
}
