using UnityEngine;
namespace DirtyChefYoga
{
	public class FrenchFries : Order
	{
		public override bool AddIngredient(Ingredient fries)
		{
			//French fries will always and only take one "Fries"
			if (fries is Fries)
			{
				ingredients.Add(fries);
				return true;
			}

			//Invalid ingredient
			Debug.LogWarning("French fries can only take a single 'Fries' ingredient");
			return false;
		}
	}
}
