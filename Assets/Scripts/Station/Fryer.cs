using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DirtyChefYoga
{
    public class Fryer : CookingStation
    {
        [SerializeField] GameObject cookedFriesPrefab;

        public override bool Interact(Ingredient ingredient)
        {
            //Only pototoes can be cooked
            if (ingredient is Potato)
            {
                //Start cooking as usual
                return base.Interact(ingredient);
            }
            return false;
        }

        protected override void HandleCooking()
        {
            base.HandleCooking();

            if (!currentIngredientCooking) return;

            if (currentIngredientCooking.cookProgress > 1.0f)
            {
                OnFinishedCooking.Invoke();
                
                //Swap out potato for fries
                var cookedFries = Instantiate(cookedFriesPrefab, workSurface.position, workSurface.rotation);
                Destroy(currentIngredientCooking.gameObject);
            }
        }
    }
}