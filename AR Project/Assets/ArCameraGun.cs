using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArCameraGun : MonoBehaviour
{
    public Camera camera;

    void Start() {
        
    }

    private void Update() {
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began) {
                Shoot();
            }
        }
    }

    public void Shoot() {
        RaycastHit hit;
        Touch touch = Input.GetTouch(0);
        Ray ray = camera.ScreenPointToRay(touch.position);

        if (Physics.Raycast(ray, out hit)) {
            if (hit.transform.tag == "Target") {
                Destroy(hit.transform.gameObject);
            }
        }
    }
}
