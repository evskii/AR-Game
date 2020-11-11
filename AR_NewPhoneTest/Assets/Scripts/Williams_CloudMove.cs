using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Williams_CloudMove : MonoBehaviour
{
    public Camera cam;
    public bool active = false;
    public Animator anim;

    private void Start() {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began) {
                CheckTouch();
            }
        }
    }

    void CheckTouch() {
        RaycastHit hit;
        Touch touch = Input.GetTouch(0);
        Ray ray = cam.ScreenPointToRay(touch.position);

        if (Physics.Raycast(ray, out hit)) {
            if (hit.transform.tag == gameObject.transform.tag) {
                if (!active) {
                    active = true;
                    anim.SetTrigger("Coversun");
                    //Williams_WaterController.instance.Freeze(true);
                }
                else {
                    active = false;
                    anim.SetTrigger("UncoverSun");
                    //Williams_WaterController.instance.Freeze(false);
                }
                
            }
        }
    }

    void FreezeWater() {
        Williams_WaterController.instance.Freeze(active);
    }
}
