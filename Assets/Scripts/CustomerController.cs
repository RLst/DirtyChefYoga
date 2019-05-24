using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : MonoBehaviour
{

    public List<GameObject> customerList = new List<GameObject>();

    public void Update() {
        if (Input.anyKeyDown) {
            CorrectOrder();
        }
    }

    public void CorrectOrder() {
        foreach (GameObject customer in customerList) {
            customer.GetComponent<Customer>().WinAnimation();
        }
    }
}
