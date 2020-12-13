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
        //Vector3 rot = new Vector3(transform.rotation.x, camTransform.rotation.y * originalRotation.y, transform.rotation.z);
        //transform.rotation = Quaternion.Euler(rot.x, rot.y, rot.z);

        //transform.LookAt(camTransform);

        var lookPos = camTransform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 0.80f);
    }
}
