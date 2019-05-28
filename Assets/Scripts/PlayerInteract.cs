using UnityEngine.Assertions;
using UnityEngine;
using System;
using UnityEngine.Events;

namespace DirtyChefYoga
{
	//Handles player interactions
	public class PlayerInteract : MonoBehaviour
	{
		[SerializeField] bool debug = true;
		[Space]

		[Header("Interactable Detection")]
		[SerializeField] Transform handAnchor;
		public Vector3 castHalfExtents = new Vector3(0.35f, 2.4f, 1f);
		public float castLength = 2.4f;
		public LayerMask interactablesMask = 9;

		public bool isHoldingItem
		{ get { return currentItem != null; } }
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
		}

		void LateUpdate()
		{
			db();
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
					if (station.Insert(currentItem))    //Try passing into the station
					{
						Debug.Log("Successfully passed " + currentItem + " to station");
						ReleaseItem();
					}
					//Rejected. Don't do anything
				}
				else    //If there's no station in front
				{
					Debug.Log("Station not found. Dropping " + currentItem);
					//The drop the item
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
					if (station.Remove(out Ingredient ingredient))   
					{
						Debug.Log("Removed " + ingredient + " from station");
						PickUpItem(ingredient);
						// currentItem = removedItem;
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

		//Clears current item
		private void ReleaseItem()
		{
			Debug.Log("Released ingredient!");
			OnRelease.Invoke();			
			currentItem = null;
		}

		//Drop the item
		private void DropItem()
		{
			Debug.Log("Dropped ingredient!");

			//Unchild
			currentItem.transform.SetParent(null);
			//Make it a physics object
			currentItem.SetPhysicsActive(true);
			//RELEASE
			ReleaseItem();
		}

		private void PickUpItem(Ingredient item)
		{
			Debug.Log("Picking up ingredient!");
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

		//Detect object of type T according to set cast paramters
		public bool DetectInteractable<T>(out T hit, Color debugColor = new Color()) where T : MonoBehaviour
		{
			var hits = Physics.OverlapBox(transform.position + transform.forward * castLength * 0.5f, castHalfExtents, transform.rotation, interactablesMask);

			//If something hit
			if (hits.Length > 0)
			{
				//Loop through all hits
				foreach (var h in hits)
				{
					hit = h.GetComponent<T>();
					//If any of them are of type T
					if (hit is T)
					{
						DrawDebugLineArray(0.25f, debugColor);
						//Return true and out T
						return true;
					}
				}
			}
			//Nothing found
			hit = null;
			return false;
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
			if (!debug) return;

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