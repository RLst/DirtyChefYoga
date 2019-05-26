using System.Collections.Generic;
using UnityEngine;
namespace DirtyChefYoga
{
	//A Food object contains a List of Ingredients
	[RequireComponent(typeof(BoxCollider))]
	[RequireComponent(typeof(Rigidbody))]
	[SelectionBase]
	public abstract class Order : MonoBehaviour	//aka. Order??
	{
		[SerializeField] Vector3 colliderSize = new Vector3(0.5f, 0, 0.5f);	//A small flat plate for the ingredients to sit on
		protected List<Ingredient> m_ingredients = new List<Ingredient>();
		public List<Ingredient> ingredients
		{
			get { return m_ingredients; }
			private set { m_ingredients = value; }
		}

		public abstract bool AddIngredient(Ingredient ingredient);

		Rigidbody rb;
		Collider col;

		protected virtual void Awake()
		{
			rb = GetComponent<Rigidbody>();
			col = GetComponent<Collider>();
		}

		protected virtual void Start() {
			(col as BoxCollider).size = colliderSize;
			col.isTrigger = true;	//Collider doesn't need to 
			SetPhysicsActive(false);
		}

		public void SetPhysicsActive(bool active)
		{
			if (active) {
				rb.constraints = RigidbodyConstraints.None;
				col.isTrigger = false;
			}
			else {
				rb.constraints = RigidbodyConstraints.FreezeAll;
				col.isTrigger = true;  //Can't be affect by other physics objects
			}
		}
	}
}

/*Brainstorm
Patty cooking scenario:
1. A food is inserted into the grill
2. The grill checks if the food consists of only ONE patty
3. The grill cooks the patty as normal

Taking the food from a station
1. if there's a food object, then return the food object

*/
