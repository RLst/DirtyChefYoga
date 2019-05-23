using UnityEngine;

namespace DirtyChefYoga
{
    public abstract class BurgerIngredient : Ingredient
    {
        [SerializeField] float m_thickness = 0.1f;
        public float thickness { get { return m_thickness; } }       //Manually set for each ingredient
    }
}