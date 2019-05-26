using System.Collections.Generic;
using UnityEngine;
namespace DirtyChefYoga
{
	public class Burger : Order
	{
		public float currentThickness = 0;      //Needed to stack the burger ingredients properly
		[SerializeField] int maxNumberOfLayers = 10;

		//Adds an ingredient onto the burger
		//Returns whether or not the ingredient was successfully added
		public override bool AddIngredient(Ingredient ingredient)
		{
			if (ingredient is BurgerIngredient)
			{
				if (ingredients.Count < maxNumberOfLayers)
				{
					////Child new ingredient to the burger at the correct position according to the burger's current thickness
					//Deactivate ingredient's physics
					ingredient.SetPhysicsActive(false);
					
					//Child ingredient to burger order object
					ingredient.transform.SetParent(this.transform);

					//Stack ingredient at the right position and random rotation
					var p = transform.position + transform.up * currentThickness;
					var r = Quaternion.AngleAxis(UnityEngine.Random.Range(0, 360), transform.up);
					ingredient.transform.SetPositionAndRotation(p, r);

					//Update burger's current thickness
					currentThickness += (ingredient as BurgerIngredient).thickness;

					//Finally add it to the array
					ingredients.Add(ingredient);

					return true;
				}
				else
				{
					Debug.LogWarning("Max ingredients reached!");
					return false;
				}
			}
			else
			{
				Debug.LogWarning("Not a burger ingredient");
				return false;
			}
		}
	}
}