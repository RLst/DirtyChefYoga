using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    // Update is called once per frame
    public void WinAnimation() {
        if (Random.Range(0,2) == 1) {
            StartCoroutine(OPose());
        } else {
            StartCoroutine(Clap());
        }
    }

    private IEnumerator OPose() {
        GetComponent<Animator>().SetBool("O-Pose", true);
        yield return new WaitForSeconds(2.0f);
        GetComponent<Animator>().SetBool("O-Pose", false);
    }

    private IEnumerator Clap() {
        GetComponent<Animator>().SetBool("Clap-Pose", true);
        yield return new WaitForSeconds(2.0f);
        GetComponent<Animator>().SetBool("Clap-Pose", false);
    }
}
