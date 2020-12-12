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

    //Game state variables
    public bool sunCovered = false;

    //Player spawning variables
    public GameObject spawnPoint;
    public GameObject player;

    public void SpawnPlayer() { //Spawn the player when called (called from NavMeshRest.cs)
        Instantiate(player, spawnPoint.transform.position, spawnPoint.transform.rotation);
    }

}
