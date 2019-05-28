using System.Collections.Generic;
using UnityEngine;
namespace DirtyChefYoga
{
	//A Food object contains a List of Ingredients
	[RequireComponent(typeof(BoxCollider))]
	[RequireComponent(typeof(Rigidbody))]
	[SelectionBase]
	public abstract class Order : MonoBehaviour
	{
		[SerializeField] Vector3 colliderSize = new Vector3(0.5f, 0.5f, 0.5f);	//A small flat plate for the ingredients to sit on
		protected List<Ingredient> m_ingredients = new List<Ingredient>();
		public List<Ingredient> ingredients
		{
			get { return m_ingredients; }
			private set { m_ingredients = value; }
		}

		public abstract bool AddIngredient(Ingredient ingredient);

		Rigidbody rb;
		Collider col;

		void Awake()
		{
			rb = GetComponent<Rigidbody>();
			col = GetComponent<Collider>();

			//Reset transform			
			transform.localRotation = Quaternion.identity;

			//Set collider size
			(col as BoxCollider).size = colliderSize;

			//Make sure it is a trigger so that it doesn't react with physics objects
			SetPhysicsActive(false);
		}

		public void SetPhysicsActive(bool active)
		{
			if (active) {
				rb.constraints = RigidbodyConstraints.None;
				col.isTrigger = false;
			}
			else {
				//Not affected by physics; floats in midair
				rb.constraints = RigidbodyConstraints.FreezeAll;
				col.isTrigger = true;
			}
		}
	}
}
