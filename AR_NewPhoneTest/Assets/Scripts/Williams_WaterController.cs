using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Williams_WaterController : MonoBehaviour
{
    /* Used to control the waters state in the game (frozen or not)
     */

    //Singleton creation
    public static Williams_WaterController instance;

    private void Awake() {
        instance = this;
    }

    
    public bool frozen = false; //Waters state (frozen or not)

    public Material materialWater; //The material that the water uses in the game
    public Color water; //Colour for when its water
    public Color ice; //Colour for when its ice

    private NavMeshObstacle blocker; //The navmeshblocker that allows the player to move over the water or not

    private void Start() {
        blocker = GetComponent<NavMeshObstacle>();
    }


    public void Freeze(bool freeze) {
        if (freeze) { //If frozen
            frozen = true;
            materialWater.SetColor("_Color", ice); 
            blocker.enabled = false; //Get rid of the blocker
        }
        else {
            frozen = false;
            materialWater.SetColor("_Color", water);
            blocker.enabled = true;
        }
    }


}
