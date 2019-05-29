using UnityEngine;
using UnityEngine.Events;

namespace DirtyChefYoga
{
	[RequireComponent(typeof(BoxCollider))]
	[SelectionBase]
	public abstract class Station : MonoBehaviour
	{
		[SerializeField] protected Transform anchor;
		public UnityEvent OnInserted, OnRemoved;

		protected Ingredient currentItem;

		/// <summary>
		/// Uses an ingredient on the station
		/// </summary>
		/// <param name="item">The ingredient to pass in</param>
		/// <returns>Returnes true if item was successfully inserted</returns>
		public abstract bool InsertItem(Ingredient item);

		/// <summary>
		/// Pop out ingredient from station
		/// </summary>
		/// <param name="item">Ingredient that will be returned</param>
		/// <returns>Returns true if item was successfully removed</returns>
		public abstract bool RemoveItem(out Ingredient item);

		public virtual void SetCurrentItem(Ingredient item)
		{
			Debug.Log("Station " + this + " taking in " + item);

			//Set current item
			currentItem = item;

			//Position to anchor with random rotation (to look cool)
			currentItem.transform.SetPositionAndRotation(anchor.position, Quaternion.AngleAxis(UnityEngine.Random.Range(0, 360), anchor.up));

			//Parent
			currentItem.transform.SetParent(this.anchor);

			//Disable physics
			currentItem.SetPhysicsActive(false);
		}
		public virtual void ReleaseCurrentItem()
		{
			currentItem = null;
		}

		//---- Debug -----
		void OnDrawGizmos()
		{
			if (anchor)
			{
				Gizmos.color = Color.yellow;
				Gizmos.DrawWireSphere(anchor.position, 0.05f);
			}
		}
	}
}
