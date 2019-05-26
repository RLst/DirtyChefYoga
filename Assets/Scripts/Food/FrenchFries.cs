using UnityEngine;
namespace DirtyChefYoga
{
	public class FrenchFries : Order
	{
		public override bool AddIngredient(Ingredient ingredient)
		{
			//French fries will always and only take one "Fries"
			if (ingredient is Fries)
			{
				//Deactivate ingredient's physics
				ingredient.SetPhysicsActive(false);

				//Child to this order
				ingredient.transform.SetParent(this.transform);

				//Position ingredient at Order root and random rotation
				var p = transform.position;
				var r = Quaternion.AngleAxis(UnityEngine.Random.Range(0, 360), transform.up);
				ingredient.transform.SetPositionAndRotation(p, r);

				//Add to the array
				ingredients.Add(ingredient);
				
				return true;
			}

			//Invalid ingredient
			Debug.LogWarning("French fries can only take a single 'Fries' ingredient");
			return false;
		}
	}
}
