using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DirtyChefYoga
{
	public class IngredientSpawner : MonoBehaviour
	{
		[SerializeField] bool debug = true;
		[Space]

        [SerializeField] bool autoStart = true;
		[SerializeField] float spawnDelay = 1f;
		public List<GameObject> ingredientPrefabs = new List<GameObject>();


        void Start()
		{
			if (autoStart) Activate();
		}

		//Spawn a random ingredient at root position
		public void SpawnRandom()
		{
			var randomPrefab = ingredientPrefabs[UnityEngine.Random.Range(0, ingredientPrefabs.Count)];
			var randomAngle = Quaternion.AngleAxis(UnityEngine.Random.Range(0, 360), Vector3.up);
			var newObject = Instantiate(randomPrefab, transform.position, randomAngle);
		}

		public void Activate()
		{
			StartCoroutine(RunSpawner(spawnDelay));
		}

		public void Deactivate()
		{
			StopCoroutine(RunSpawner(spawnDelay));
		}

		//Spawns indefinitely at set 
		IEnumerator RunSpawner(float seconds)
		{
			while (true)
			{
				yield return new WaitForSeconds(seconds);
				SpawnRandom();
			}
		}

		//------------------ DEBUG ---------------------
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