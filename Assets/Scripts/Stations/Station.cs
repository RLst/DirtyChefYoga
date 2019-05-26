using UnityEngine;
using UnityEngine.Events;

namespace DirtyChefYoga
{
	[RequireComponent(typeof(BoxCollider))]
	public abstract class Station : MonoBehaviour
	{
		[SerializeField] protected Transform workSurface;
		public UnityEvent OnInteract, OnRemoved;

        protected Ingredient currentItem;

		//Uses an ingredient on the station
		//Params: Returns true if item was successfully inserted
		public abstract bool Insert(Ingredient item);

		//Pop ingredient from station
		//Params: Returns true if current item was successfully removed
		public abstract bool Remove(out Ingredient item);

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
