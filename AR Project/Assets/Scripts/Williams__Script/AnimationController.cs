using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator anim;

    private void Start() {
        anim = GetComponent<Animator>();

    }

    public void Twerk() {
        anim.SetTrigger("Twerk");
    }

    public void Idle() {
        anim.SetBool("Idle", true);
        anim.SetBool("Walking", false);
    }

    public void Walk() {
        anim.SetBool("Walking", true);
        anim.SetBool("Idle", false);
    }
}
