using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace DirtyChefYoga
{
	//Handles player interactions
	public partial class PlayerActions : MonoBehaviour
	{
		[SerializeField] bool debug = true;
		[Space]

		[Header("Interactable Detection")]
		[SerializeField] Transform handAnchor;
		public Vector3 castHalfExtents = new Vector3(0.35f, 2.4f, 1f);
		public float castLength = 2.4f;
		public LayerMask interactablesMask = 9;

		public bool isHoldingItem { get { return currentItem != null; } }
		Ingredient currentItem;

		private PlayerInput input;
		[SerializeField] UnityEvent OnPickup, OnRelease;

		void Start()
		{
			Assert.IsNotNull(handAnchor, "No hand transform found!");
			input = GetComponent<PlayerInput>();
		}

		void Update()
		{
			HandleInteractions();
			if (debug) HandleHighlights();
		}

		void LateUpdate()
		{
			if (debug) db();
		}

		private void HandleInteractions()
		{
			if (!input.pickedUp) return;

			//NOTE: STATIONS ALWAYS HAVE HIGHER PRIORITY THAN AN INGREDIENT

			//If holding item
			if (isHoldingItem)
			{
				//If there's a station in front of you then interact with it using the ingredient
				if (DetectInteractable<Station>(out Station station, Color.yellow))
				{
					if (station.InsertItem(currentItem)) //Try passing into the station
					{
						Debug.Log("Successfully passed " + currentItem + " to station");
						ReleaseItem();
					}
				}
				else
				{
					Debug.Log("Station not found. Dropping " + currentItem);
					DropItem();
				}
			}
			//If not holding any item
			else
			{
				//If a station is found
				if (DetectInteractable<Station>(out Station station, Color.yellow))
				{
					//Try removing ingredient
					if (!station) Debug.Log("Station is null");
					if (station.RemoveItem(out Ingredient item))
					{
						Debug.Log("Removed " + item + " from station");
						PickUpItem(item);
					}
					//NOTE: For this to work properly, benches would have to be able to take orders as well
					//Might have to combine all ingredients into a Food class
					// //Try removing an order
					// else if (station.Remove(out Order order))
					// {
					// 	Debug.Log("Removed " + order + " from station");
					// 	PickUpOrder(order);
					// }
				}
				else
				{
					//If an ingredient found then pick it up
					if (DetectInteractable<Ingredient>(out Ingredient item, Color.green))
					{
						Debug.Log("Picked up a " + item);
						PickUpItem(item);
					}
				}
			}
		}

		private void PickUpItem(Ingredient item)
		{
			OnPickup.Invoke();
			//Set current item
			currentItem = item;
			//Move to the hand
			currentItem.transform.SetPositionAndRotation(handAnchor.position, handAnchor.rotation);
			//Set it as a child
			currentItem.transform.SetParent(this.handAnchor);
			//Deactivate physics
			currentItem.SetPhysicsActive(false);
		}

		//Drop the item
		public void DropItem()
		{
			//Unchild
			currentItem?.transform.SetParent(null);
			//Make it a physics object
			currentItem?.SetPhysicsActive(true);
			//RELEASE
			ReleaseItem();
		}

		//Unsets currentItem
		private void ReleaseItem()
		{
			OnRelease.Invoke();
			currentItem = null;
		}

		//Detect object of type T according to set cast paramters
		public bool DetectInteractable<T>(out T hit, Color debugColor = new Color()) where T : MonoBehaviour
		{
			Collider[] hits = Physics.OverlapBox(transform.position + transform.forward * castLength * 0.5f, castHalfExtents, transform.rotation, interactablesMask);

			//If something hit
			if (hits.Length > 0)
			{
				var lHits = new List<Collider>(hits);
				Debug.Log("Initial");
				printListOfHits(lHits);

				//Remove objects that aren't of target type T
				lHits.RemoveAll(col => !col.GetComponent<T>());
				Debug.Log("Filtered");
				printListOfHits(lHits);

				if (lHits.Count > 0)
				{
					//Sort from lowest to greatest alignment. Last element will be most aligned
					lHits.Sort((x, y) =>
						Vector3.Dot(Vector3.Normalize(x.transform.position - transform.position), transform.forward). //Does it need to be normalized?
						CompareTo(Vector3.Dot(Vector3.Normalize(y.transform.position - transform.position), transform.forward)));
					Debug.Log("Sorted");
					printListOfHits(lHits);

					//Return most aligned element (last)
					hit = lHits[lHits.Count - 1].GetComponent<T>();
					Debug.Log("MOST ALIGNED HIT: " + lHits[lHits.Count - 1]);

					//SUCCESS!
					return true;
				}
			}
			//Nothing found
			Debug.Log("Nothing Found");
			hit = null;
			return false;

			void printListOfHits(List<Collider> listOfHits)
			{
				Debug.Log("Element count: " + listOfHits.Count);
				foreach (var h in listOfHits)
				{
					Debug.Log(">>> " + h);
				}
			}
		}

		void DrawDebugLineArray(float arraySpacing, Color color)
		{
			for (float i = -castHalfExtents.y; i < castHalfExtents.y; i += arraySpacing)
			{
				for (float j = -castHalfExtents.x; j < castHalfExtents.x; j += arraySpacing)
				{
					var t = transform;
					var from = t.position + t.up * i + t.right * j;
					var to = t.position + t.up * i + t.right * j + t.forward * castLength;
					Debug.DrawLine(from, to, color);
				}
			}
		}

		void OnGUI()
		{
			if (!debug) return;

			GUILayout.Label("Player Controller");
			if (currentItem != null)
			{
				GUILayout.Label("Holding a " + currentItem.name);
			}

			if (currentHighlightObject) GUILayout.Label("Current Highlighted Object + " + currentHighlightObject);
		}

		void OnDrawGizmos()
		{
			Gizmos.color = Color.red;
			Transform t = transform;
			for (float i = -castHalfExtents.y; i < castHalfExtents.y; i += 0.2f)
			{
				Vector3 leftFrom = t.position + t.up * i - t.right * castHalfExtents.x;
				Vector3 rightFrom = t.position + t.up * i + t.right * castHalfExtents.x;
				Vector3 leftTo = t.position + t.up * i - t.right * castHalfExtents.x + t.forward * castLength;
				Vector3 rightTo = t.position + t.up * i + t.right * castHalfExtents.x + t.forward * castLength;
				Gizmos.DrawLine(leftFrom, leftTo);
				Gizmos.DrawLine(rightFrom, rightTo);
			}
		}

		private void db()
		{
			if (DetectInteractable<Ingredient>(out Ingredient ingredient))
			{
				Debug.Log("Detecting ingredient!: " + ingredient);
			}
			else if (DetectInteractable<Station>(out Station station))
			{
				Debug.Log("Detecting station!: " + station);
			}
		}
	}
}

// class MostCentreOfView : IComparer<Collider>
// {
// 	public int Compare(Collider x, Collider y)
// 	{
// 		var dotX = x.transform.position - 
// 		throw new NotImplementedException();
// 	}
// }