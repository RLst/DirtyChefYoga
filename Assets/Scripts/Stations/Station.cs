using UnityEngine;
using UnityEngine.Events;

namespace DirtyChefYoga
{
	[RequireComponent(typeof(BoxCollider))]
	public abstract class Station : MonoBehaviour
	{
		[SerializeField] protected Transform workSurface;
		public UnityEvent OnInteract;

		//It is assumed the ingredient passed in will be NOT be null
		public abstract bool Interact(Ingredient ingredient);

		void OnDrawGizmos()
		{
			if (workSurface)
			{
				Gizmos.color = Color.yellow;
				Gizmos.DrawWireSphere(workSurface.position, 0.1f);
			}
		}
	}
}
