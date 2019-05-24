using System.Collections.Generic;
using UnityEngine;

namespace DirtyChefYoga
{
    public class Burger : Food
    {
        float currentThickness = 0;      //Needed to stack the burger ingredients properly
        [SerializeField] int maxNumberOfLayers = 10;

        //Adds an ingredient onto the burger
        //Returns whether or not the ingredient was successfully added
        public override bool AddIngredient(Ingredient ing)
        {
            //Make sure it's a burger ingredient
            var bi = ing as BurgerIngredient;
            if (!bi)
            {
                Debug.LogWarning("Not a burger ingredient");
                return false;
            }

            if (m_ingredients.Count < maxNumberOfLayers)
            {
                ////Child new ingredient to the burger at the correct position according to the burger's current thickness
                //Move ingredient to the right location in the world
                bi.transform.position = this.transform.position + Vector3.up * currentThickness;

                //Child the ingredient
                bi.transform.SetParent(this.transform);

                //Update the current thickness
                currentThickness += bi.thickness;

                //Finally add it to the array
                m_ingredients.Add(bi);

                return true;
            }
            else
            {
                Debug.LogWarning("Max ingredients reached!");
                return false;
            }
        }
    }

}