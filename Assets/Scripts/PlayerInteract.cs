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

		public bool isHoldingItem
        	{ get { return currentItem != null; } }

		PlayerInput input;
		Ingredient currentItem;

		void Start()
		{
			Assert.IsNotNull(anchor, "No hand transform found!");
			input = GetComponent<PlayerInput>();
		}

		void Update()
		{
			HandleInteractions();
			// db();
		}

		private void HandleInteractions()
		{
			if (input.interacted)
			{
				//If holding ingredient
				if (isHoldingItem)
				{
					//If there's a station in front of you then interact with it using the ingredient
					if (DetectInteractable<Station>(out Station stationHit))
					{
						Debug.Log("Station found");
						//Pass ingredient to station
						if (stationHit.Insert(currentItem))
						{
							Debug.Log("Successfully passed ingredient to station");
							ReleaseIngredient();
						}
					}
					//Otherwise drop the ingredient
					else
					{
						ReleaseIngredient();
					}
				}
				//If not holding ingredient
				else
				{
					

					//If a station is found
					// if (DetectInteractable<Station>(out Station station))
					// {
					// 	//Remove ingredient instead of interacting with it
					// 	if (station.Remove(out Ingredient item))
					// 	{
					// 	}
					// }

					//If ingredient found
					if (DetectInteractable<Ingredient>(out Ingredient hit))
					{
						PickUpIngredient(hit);
					}
					//Do nothing if you're not holding anything
				}

			}
		}

		private void ReleaseIngredient()
		{
			Debug.Log("Drop the item!");

			

			// currentItem

			//Unchild
			currentItem.transform.SetParent(null);
			//Make it a physics object
			currentItem.SetPhysicsActive(true);
			//RELEASE
			currentItem = null;
		}

		private void PickUpIngredient(Ingredient ingredientHit)
		{
			Debug.Log("Picking up ingredient!");

			////Pick it up
			//Set as picked up
			currentItem = ingredientHit;
			//Move to the hand
			currentItem.transform.SetPositionAndRotation(anchor.transform.position, anchor.transform.rotation);
			//Set it as a child
			currentItem.transform.SetParent(this.transform);
			//Deactivate physics
			currentItem.SetPhysicsActive(false);
		}

		//Detect object of type T according to set cast paramters
		bool DetectInteractable<T>(out T hit) where T : MonoBehaviour
		{
			bool isHit = Physics.BoxCast(transform.position, castHalfExtents, transform.forward, out RaycastHit hitInfo, Quaternion.LookRotation(transform.forward), castLength);

			//If something hit
			if (isHit)
			{
				// Debug.DrawRay(transform.position, transform.forward * 10, Color.blue);
				hit = hitInfo.collider.GetComponent<T>();
				//Get found component
				if (hit is T) //item is the right type!
				{
					DrawDebugLineArray(0.25f, Color.green);
					//set found object
					return true;
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
			if (debug)
			{
				GUILayout.Label("Player Controller");
				if (currentItem != null)
				{
					GUILayout.Label("Holding a " + currentItem.name);
				}
			}
		}

		void OnDrawGizmos() 
		{
			Gizmos.color = Color.red;
			Transform t = transform;
			for (float i = -castHalfExtents.y; i < castHalfExtents.y; i += 0.25f)
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