using System.Collections.Generic;
using UnityEngine;
namespace DirtyChefYoga
{
    public abstract class Food : MonoBehaviour
    {
        protected List<Ingredient> m_ingredients = new List<Ingredient>();
        public List<Ingredient> ingredients
        {
            get { return m_ingredients; }
            private set { m_ingredients = value; }
        }

        public abstract bool AddIngredient(Ingredient ing);
    }
}
