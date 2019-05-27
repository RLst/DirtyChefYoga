using System.Collections.Generic;
using UnityEngine;
namespace DirtyChefYoga
{
	public class Burger : Order
	{
		float currentThickness = 0;      //Needed to stack the burger ingredients properly
		[SerializeField] int maxNumberOfLayers = 10;

		//Adds an ingredient onto the burger
		//Returns whether or not the ingredient was successfully added
		public override bool AddIngredient(Ingredient @in)
		{
			if (@in is BurgerIngredient)
			{
				if (ingredients.Count < maxNumberOfLayers)
				{
					////Child new ingredient to the burger at the correct position according to the burger's current thickness
					
					//Deactivate ingredient's physics
					@in.SetPhysicsActive(false);

					//Stack ingredient at the right position and random rotation
					Vector3 p = transform.position + Vector3.up * currentThickness;
					Quaternion r = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0);
					@in.transform.SetPositionAndRotation(p, r);
					// Quaternion r = Quaternion.AngleAxis(UnityEngine.Random.Range(0, 360), transform.up);		//THIS WAS THE DAMN CULPRIT!!!

					//Child ingredient to burger order object
					@in.transform.SetParent(this.transform);

					//Update burger's current thickness
					currentThickness += (@in as BurgerIngredient).thickness;

					//Finally add it to the array
					ingredients.Add(@in);

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