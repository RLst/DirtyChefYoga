using UnityEngine;
namespace DirtyChefYoga
{
	//Continually find and highlight interactable items in front of player
	public class InteractableHighlighter : MonoBehaviour
	{
		[SerializeField] float emmisionAmount = 0.5f;

		Vector3 castHalfExtents;
		float castLength;
		LayerMask interactablesMask;
		PlayerInteract interacter;

		void Start() 
		{
			interacter = GetComponent<PlayerInteract>();
			//Get cast settings
			castHalfExtents = interacter.castHalfExtents;
			castLength = interacter.castLength;
			interactablesMask = interacter.interactablesMask;
		}

		void Update() 
		{
			var hits = Physics.OverlapBox(transform.position + transform.forward * castLength * 0.5f,
				castHalfExtents, transform.rotation, interactablesMask);
			
			//Only highlight stations?
			if (hits.Length > 0)
			{
				foreach (var h in hits)
				{
					if (!h.GetComponent<Station>())
					{
						var material = h.GetComponent<MeshRenderer>().material;
						material.SetFloat("_Emission", emmisionAmount);
					}
				}
			}
		}
	}
}
