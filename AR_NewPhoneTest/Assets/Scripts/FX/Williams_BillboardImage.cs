using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Williams_BillboardImage : MonoBehaviour
{
    private Transform camTransform;

    Quaternion originalRotation;

    private void Start() {
        GameObject arCamera = GameObject.Find("ARCamera"); //Find the ARCamera in the scene
        camTransform = arCamera.transform;

        originalRotation = transform.rotation;
    }

    private void Update() {
        transform.rotation = camTransform.rotation * originalRotation;
    }
}
