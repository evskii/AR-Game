using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    //Trinkets
    public int trinketsCollected = 0;
    public Text trinketText;

    public void DisplayTrinketText() {
        trinketText.enabled = true;
        trinketText.text = trinketsCollected.ToString() + " / 2 Secrets Collected!";
        StartCoroutine(HideTrinketText());
    }

    IEnumerator HideTrinketText() {
        yield return new WaitForSeconds(3);
        trinketText.enabled = false;
    }


}
