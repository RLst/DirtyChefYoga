using UnityEngine;

namespace DirtyChefYoga
{
    public abstract class Ingredient : MonoBehaviour
    {
        int stackingOrder;      //Maybe...
        public float thickness = 0.1f;       //Manually set for each ingredient
    }
}