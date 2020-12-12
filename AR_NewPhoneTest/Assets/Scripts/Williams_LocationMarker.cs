using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Williams_LocationMarker : MonoBehaviour
{
    public GameObject marker;
    public GameObject dot;

    public float animHeight;
    public float lerpAmt;
    public float stoppingDistance;
    private float currentHeight;
    private float startHeight;
    
    private float destinationHeight;

    private void Start() {
        startHeight = marker.transform.position.y;
        destinationHeight = startHeight + animHeight;
    }

    private void Update() {
        currentHeight = marker.transform.position.y;
        float yPos = Mathf.Lerp(currentHeight, destinationHeight, lerpAmt);
        Vector3 pos = new Vector3(marker.transform.position.x, yPos, marker.transform.position.z);
        marker.transform.position = pos;

        if (Mathf.Abs((marker.transform.position.y - destinationHeight)) < stoppingDistance) {
            if(destinationHeight > startHeight) {
                destinationHeight -= animHeight * 2;
            } else {
                destinationHeight += animHeight * 2;
            }
            
        }
    }

    public void DestroyMe() {
        Destroy(gameObject);
    }
}
