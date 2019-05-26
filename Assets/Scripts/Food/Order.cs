using System.Collections.Generic;
using UnityEngine;
namespace DirtyChefYoga
{
	//A Food object contains a List of Ingredients
	[RequireComponent(typeof(BoxCollider))]
	[RequireComponent(typeof(Rigidbody))]
	public abstract class Order : MonoBehaviour	//aka. Order??
	{
		protected List<Ingredient> m_ingredients = new List<Ingredient>();
		public List<Ingredient> ingredients
		{
			get { return m_ingredients; }
			private set { m_ingredients = value; }
		}

		public abstract bool AddIngredient(Ingredient ingredient);
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
