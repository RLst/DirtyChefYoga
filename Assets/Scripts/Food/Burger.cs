using System.Collections.Generic;
using UnityEngine;

namespace DirtyChefYoga
{

    public class Burger : MonoBehaviour
    {
        public float currentThickness = 0;
        [SerializeField] int maxNumberOfIngredients = 5;
        List<Ingredient> ingredients = new List<Ingredient>();   //Bottom ingredients are first, top ingredients are last\


        //Adds an ingredient onto the burger
        //Returns whether or not the ingredient was successfully added
        public bool AddIngredient(Ingredient item)
        {
            if (ingredients.Count < maxNumberOfIngredients)
            {
                ////Child new ingredient to the burger at the correct position according to the burger's current thickness
                //Move ingredient to the right location in the world
                item.transform.position = this.transform.position + Vector3.up * currentThickness;

                //Child the ingredient
                item.transform.SetParent(this.transform);

                //Update the current thickness
                currentThickness += item.thickness;

                //Finally add it to the array
                ingredients.Add(item);

                return true;
            }
            else
            {
                Debug.Log("Max ingredients reached!");
                return false;
            }
        }

    }

}