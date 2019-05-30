
using System.Collections.Generic;
using UnityEngine;
namespace DirtyChefYoga
{
	public class MoveToCenter : MonoBehaviour
	{
		[Header("Must Add All Players To This List")]
		[SerializeField] Transform[] objects;
		// [SerializeField] LayerMask layer;
		
		Vector3 center;

		// void Awake()
		// {
		// 	objects = FindTransformsOnLayer(layer);
		// 	Debug.Log("Transforms" + objects.Length);
		// }

		void Update()
		{
			MoveToCenterAverage();
		}

		private void MoveToCenterAverage()
		{
			//Calculate center
			if (objects.Length > 1)
			{
				var sumPosition = new Vector3();
				foreach (var p in objects)
					sumPosition += p.position;
				center = sumPosition / (float)objects.Length;
			}
			else
			{
				center = objects[0].position;
			}
			//Move to center
			this.transform.position = center;
		}

		Transform[] FindTransformsOnLayer(LayerMask layer)
		{
			var tfArray = FindObjectsOfType<Transform>();
			var tfList = new List<Transform>();
			for (var i = 0; i < tfArray.Length; i++)
			{
				if (tfArray[i].gameObject.layer == layer)
				{
					tfList.Add(tfArray[i]);
				}
			}
			if (tfList.Count == 0) return null;
			return tfList.ToArray();
		}
	}
}