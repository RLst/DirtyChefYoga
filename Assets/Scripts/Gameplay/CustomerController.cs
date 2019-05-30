using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DirtyChefYoga
{
	public class CustomerController : MonoBehaviour
	{
		public List<GameObject> customerList = new List<GameObject>();

		public void PlayWinAnimation()
		{
			foreach (GameObject customer in customerList)
			{
				customer.GetComponent<Customer>().WinAnimation();
			}
		}
	}
}