using UnityEngine;
namespace DirtyChefYoga
{
	//Continually find and highlight interactable items in front of player
	public partial class PlayerActions : MonoBehaviour
	{
		[Header("Debug Look Highlighting")]
		[SerializeField] Color emissiveColor;
		Color noEmissiveColor = Color.black;

		MonoBehaviour currentHighlightObject;

		void HandleHighlights()
		{
			//For debugging only. Doesn't work well. Buggy. Who cares
			if (isHoldingItem)
			{
				//Detect stations
				if (DetectInteractable<Station>(out Station stationHit))
				{
					//Unhighlight the old object if it exists
					currentHighlightObject?.GetComponentInChildren<MeshRenderer>().material.SetFloat("_EmissionOn", 0f);

					currentHighlightObject = stationHit;

					currentHighlightObject.GetComponentInChildren<MeshRenderer>().material.SetFloat("_EmissionOn", 1f);
				}
				else
					UnHighlightEverything();
			}
			else
			{
				// //Unhighlight any stations
				// if (currentHighlightObject is Station) currentHighlightObject.GetComponentInChildren<MeshRenderer>().material.SetFloat("_EmissionOn", 0f);
				// currentHighlightObject = null;

				//Detect ingredients
				if (DetectInteractable<Ingredient>(out Ingredient ingredientHit))
				{
					currentHighlightObject?.GetComponentInChildren<MeshRenderer>().material.SetColor("_EmissiveColor", noEmissiveColor);

					currentHighlightObject = ingredientHit;

					noEmissiveColor = currentHighlightObject.GetComponentInChildren<MeshRenderer>().material.GetColor("_EmissiveColor");
					currentHighlightObject.GetComponentInChildren<MeshRenderer>().material.SetColor("_EmissiveColor", emissiveColor);
				}
				else
					UnHighlightEverything();
			}

			void UnHighlightEverything()
			{
				if (currentHighlightObject)
				{
					//Unhighlight any ingredients
					if (currentHighlightObject is Ingredient) 
						currentHighlightObject.GetComponentInChildren<MeshRenderer>().material.SetColor("_EmissiveColor", noEmissiveColor);
					else if (currentHighlightObject is Station)
						currentHighlightObject.GetComponentInChildren<MeshRenderer>().material.SetFloat("_EmissionOn", 0f);
	
					currentHighlightObject = null;
				}
			}
		}

	}
}
