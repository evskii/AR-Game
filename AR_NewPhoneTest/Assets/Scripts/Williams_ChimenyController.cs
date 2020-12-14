using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Williams_ChimenyController : MonoBehaviour
{
    private Camera cam; //Camera used for raycasting
    public GameObject smokeParticles;

    private void Start() {

        GameObject arCamera = GameObject.Find("ARCamera"); //Find the ARCamera in the scene
        cam = arCamera.GetComponent<Camera>(); //and reference our camera to it

    }


    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit)) { //If our raycast makes contact
                if (hit.transform.tag == "Chimney") {
                    //Rain
                    smokeParticles.GetComponent<ParticleSystem>().Play();
                    StartCoroutine(StopSmoke());

                }
            }
        }

    }

    IEnumerator StopSmoke() {
        yield return new WaitForSeconds(0.5f);
        smokeParticles.GetComponent<ParticleSystem>().Stop();
    }
}
