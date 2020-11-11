using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Williams_WaterController : MonoBehaviour
{
    //Singleton creation
    public static Williams_WaterController instance;

    private void Awake() {
        instance = this;
    }


    public bool frozen = false;

    public Material materialWater;
    public Color water;
    public Color ice;

    public BoxCollider blocker;


    public void Freeze(bool freeze) {
        if (freeze) {
            frozen = true;
            materialWater.SetColor("_Color", ice);
            blocker.enabled = false;
        }
        else {
            frozen = false;
            materialWater.SetColor("_Color", water);
            blocker.enabled = true;
        }
    }

}
