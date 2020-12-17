using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Williams_PlayerSpawn : MonoBehaviour
{
    private Animator anim;
    public GameObject player;

    private bool landed = false;

    private void Start() {
        anim = GetComponentInChildren<Animator>();
    }


    private void OnCollisionEnter(Collision collision) {
        if(collision.transform.tag == "Ground" && !landed) { 
            anim.SetTrigger("Impact");
            StartCoroutine(ChangeGameObject());
            landed = true;
        }
    }

    IEnumerator ChangeGameObject() {
        yield return new WaitForSeconds(3.5f);
        Instantiate(player, transform.position, transform.rotation);
        Williams_CabinMan.instance.GoOutside();
        Destroy(gameObject);

    }

    


}
