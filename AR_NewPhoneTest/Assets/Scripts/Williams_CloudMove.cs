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
    public bool active = false; //Wether the cloud is active (covering sun) or !active (idle)
    public Animator anim; //Suns animator (used to move its position, may change this in the future(animation independent is nicer)

    public LayerMask cloudMask; //layer that the cloud is on (not completely neccesary cause of the transform.tag check but hey ;)

    private void Start() {
        anim = GetComponent<Animator>(); //Self explanitory

        GameObject arCamera = GameObject.Find("ARCamera"); //Find the ARCamera in the scene
        cam = arCamera.GetComponent<Camera>(); //and reference our camera to it

    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit)) { //If our raycast makes contact
                if (hit.transform.tag == "Cloud") {
                    if (!active) { //If the coud is not already active
                        active = true; //Set active
                        anim.SetTrigger("Coversun"); //Animate to cover the sun
                        
                    } else { //Do the opposite x
                        active = false;
                        anim.SetTrigger("UncoverSun");
                        


                    }
                }
            }
        }
    }

    void FreezeWater() {
        Williams_WaterController.instance.Freeze(active); //Tells the water gameobject to freeze
        GameManager.instance.sunCovered = active;
        if (active) {
            Williams_CabinMan.instance.GoInside();
        } else {
            Williams_CabinMan.instance.GoOutside();
        }
        
    }
}
