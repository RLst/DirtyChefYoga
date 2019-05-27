using UnityEngine;

namespace DirtyChefYoga
{
	public abstract class BurgerIngredient : Ingredient
	{
		public float thickness
		{
			//Set the thickness via the collider; Kill two birds with one stone!
			get
			{
				// Debug.Log("Collider size: " + (col as BoxCollider).size);	
				return (col as BoxCollider).size.y;		//MUST CAST TO A BOX COLLIDER!!!
			}
		}
	}
}