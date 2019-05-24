using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Customer : MonoBehaviour
{
	Animator anim;
	void Start() {
		anim = GetComponent<Animator>();
	}
    public void WinAnimation() {
        if (Random.Range(0,2) == 1) {
            StartCoroutine(OPose());
        } else {
            StartCoroutine(Clap());
        }
    }

    private IEnumerator OPose() 
	{
        anim.SetBool("O-Pose", true);
        yield return new WaitForSeconds(2.0f);
        anim.SetBool("O-Pose", false);
    }

    private IEnumerator Clap() 
	{
        anim.SetBool("Clap-Pose", true);
        yield return new WaitForSeconds(2.0f);
        anim.SetBool("Clap-Pose", false);
    }
}
