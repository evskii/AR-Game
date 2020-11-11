using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Singleton Creation
    public static GameManager instance;

    private void Awake() {
        instance = this;
    }

    //Interactive Variables
    public bool sunCovered = false;

}
