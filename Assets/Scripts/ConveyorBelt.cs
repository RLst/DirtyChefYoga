using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DirtyChefYoga
{
	public class ConveyorBelt : MonoBehaviour
	{
		public float speed = 5f;
		[SerializeField] Vector3 direction = new Vector3(-1, 0, 0);

		//Move physics objects in direction @ speed
		void OnCollisionStay(Collision item)
		{
			item.gameObject.GetComponent<Rigidbody>().velocity = speed * direction * Time.deltaTime;
		}
	}
}