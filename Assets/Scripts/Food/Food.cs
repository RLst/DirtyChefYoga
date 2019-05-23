using System.Collections.Generic;
using UnityEngine;
namespace DirtyChefYoga
{
    public abstract class Food : MonoBehaviour
    {
        protected List<Ingredient> ingredients = new List<Ingredient>();

        public abstract bool AddIngredient(Ingredient ing);
    }
}
