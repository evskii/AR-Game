using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Williams_SunAndMoon : MonoBehaviour
{
    public static Williams_SunAndMoon instance;

    private void Awake() {
        instance = this;
    }

    public void Rotate() {
        if (sunsOut) {
            anim.SetTrigger("SunOut");
        } else {
            anim.SetTrigger("MoonOut");
        }
    }

    private Camera cam; //Camera used for raycasting

    public bool sunsOut = true;
    public GameObject sun;
    public GameObject moon;
    private Animator anim;



    private void Start() {

        GameObject arCamera = GameObject.Find("ARCamera"); //Find the ARCamera in the scene
        cam = arCamera.GetComponent<Camera>(); //and reference our camera to it

        anim = GetComponent<Animator>();
    }

    


    void Update() {

        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit)) { //If our raycast makes contact
                if (hit.transform.tag == "Sun") {
                    sunsOut = !sunsOut;
                    FreezeWater();
                    Rotate();
                }
            }
        }
    }

    void FreezeWater() {
        Williams_WaterController.instance.Freeze(!sunsOut); //Tells the water gameobject to freeze
        GameManager.instance.sunCovered = !sunsOut;
        if (!sunsOut) {
            Williams_CabinMan.instance.GoInside();
        } else {
            Williams_CabinMan.instance.GoOutside();
        }

    }
}
