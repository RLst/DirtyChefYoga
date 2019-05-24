using UnityEngine;

namespace DirtyChefYoga
{
    public abstract class BurgerIngredient : Ingredient
    {
        public float thickness { 
			//Set the thickness via the collider
			//Kill two birds with one stone!
			get { return col.bounds.size.y; }}	
    }
}