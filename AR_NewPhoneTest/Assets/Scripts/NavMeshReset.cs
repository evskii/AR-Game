using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshReset : MonoBehaviour
{
    public static NavMeshReset instance;

    private void Awake() {
        instance = this;
    }

    /* This script re generates the navmesh for the level
     * in runtime. It means that when the geometry of the
     * level spawns in, the navmesh will fit to whatever 
     * rotation the reference area is rather than staying
     * in Unity units. 
     */

    public NavMeshSurface surface; //References a gameobject in the scene with a "NavMeshSurface" component on it

    private void Update() {
        //PC Debug when testing mesh regeneration
        //if (Input.GetKey(KeyCode.B)) {
        //    RebuildMesh();
        //}
    }

    public void RebuildMesh() {
        StartCoroutine(buildDelay());
    }

    IEnumerator buildDelay() {
        yield return new WaitForSeconds(0.5f); //Allows a delay so the navmesh generates when the geometry is settled
        surface.BuildNavMesh();
        yield return new WaitForSeconds(0.5f); //Another delay so the player spawns in when the navmesh is fully generated
        GameManager.instance.SpawnPlayer();
    }

    public void ImmediateRebuildMesh() {
        surface.BuildNavMesh();
    }
}
