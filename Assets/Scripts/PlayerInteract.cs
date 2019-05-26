using UnityEngine.Assertions;
using UnityEngine;
using System;

namespace DirtyChefYoga
{
	//Handles player interactions
	public class PlayerInteract : MonoBehaviour
	{
		[SerializeField] bool debug = true;
		[Space]
		[SerializeField] Transform anchor;
		[SerializeField] Vector3 castHalfExtents = new Vector3(0.45f, 2, 0.2f);
		[SerializeField] float castLength = 2.4f;
		[SerializeField] LayerMask interactablesMask;

		public bool isHoldingItem
        	{ get { return currentItem != null; } }
		Ingredient currentItem;

		private PlayerInput input;

		void Start() {
			Assert.IsNotNull(anchor, "No hand transform found!");
			input = GetComponent<PlayerInput>();
		}

		void Update() {
			HandleInteractions();
		}

		void LateUpdate() {
			db();
		}

		private void HandleInteractions()
		{
			if (!input.interacted) return;

				//NOTE: STATIONS ALWAYS HAVE HIGHER PRIORITY THAN AN INGREDIENT

				//If holding item
				if (isHoldingItem)
				{
					//If there's a station in front of you then interact with it using the ingredient
					if (DetectInteractable<Station>(out Station stationHit, Color.yellow))
					{
						if (stationHit.Insert(currentItem))	//Try passing into the station
						{
							Debug.Log("Successfully passed item to station");
							ReleaseItem();
						}
						//Rejected. Don't do anything
					}
					else	//If there's no station in front
					{
						Debug.Log("Station not found. Dropping item");
						//The drop the item
						ReleaseItem();
					}
				}
				//If not holding any item
				else
				{
					//If a station is found
					if (DetectInteractable<Station>(out Station station, Color.yellow))
					{
						if (station.Remove(out Ingredient removedItem))	//Try removing ingredient
						{
							Debug.Log("Successfully removed ingredient from station");
							PickUpItem(removedItem);
							// currentItem = removedItem;
						}
					}
					else
					{
						//If an ingredient found then pick it up
						if (DetectInteractable<Ingredient>(out Ingredient foundItem, Color.green))
						{
							Debug.Log("Picked up item");
							PickUpItem(foundItem);
						}
					}
				}

		}

		private void ReleaseItem()
		{
			Debug.Log("Release ingredient!");

			//Unchild
			currentItem.transform.SetParent(null);
			//Make it a physics object
			currentItem.SetPhysicsActive(true);
			//RELEASE
			currentItem = null;
		}

		private void PickUpItem(Ingredient item)
		{
			Debug.Log("Picking up ingredient!");

			//Set current item
			currentItem = item;
			//Move to the hand
			currentItem.transform.SetPositionAndRotation(anchor.transform.position, anchor.transform.rotation);
			//Set it as a child
			currentItem.transform.SetParent(this.anchor);
			//Deactivate physics
			currentItem.SetPhysicsActive(false);
		}

		//Detect object of type T according to set cast paramters
		bool DetectInteractable<T>(out T hit, Color debugColor = new Color()) where T : MonoBehaviour
		{
			var hits = Physics.OverlapBox(transform.position + transform.forward * castLength * 0.5f, castHalfExtents, transform.rotation, interactablesMask);
			// bool isHit = Physics.BoxCast(transform.position + castOffset, castHalfExtents, transform.forward, out RaycastHit hitInfo, Quaternion.LookRotation(transform.forward), castLength);

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
						Debug.Log("Hit item: " + hit);
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
					var from = t.position + t.up*i + t.right*j;
					var to = t.position + t.up*i + t.right*j + t.forward*castLength;
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
			for (float i = -castHalfExtents.y; i < castHalfExtents.y; i += 0.5f)
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