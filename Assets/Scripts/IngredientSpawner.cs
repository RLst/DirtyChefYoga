using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DirtyChefYoga
{
	public class IngredientSpawner : MonoBehaviour
	{
		[SerializeField] bool debug = true;

		[Space]
		public List<GameObject> ingredientPrefabs = new List<GameObject>();

		//Spawn a random ingredient at root position
		public void SpawnRandom()
		{
			var randomPrefab = ingredientPrefabs[UnityEngine.Random.Range(0, ingredientPrefabs.Count)];
			var randomAngle = Quaternion.AngleAxis(UnityEngine.Random.Range(0, 360), Vector3.up);
			var newObject = Instantiate(randomPrefab, transform.position, randomAngle);
		}

		void OnDrawGizmos()
		{
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(transform.position, 0.9f);
		}

		void OnGUI()
		{
			if (debug)
				if (GUILayout.Button("Spawn Random Ingredient"))
					SpawnRandom();
		}
	}
}