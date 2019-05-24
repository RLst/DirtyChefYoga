using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DirtyChefYoga
{
    public abstract class CookingStation : Station
    {
        [SerializeField] float cookAmount = 0.001f;
        // [SerializeField] float cookTime = 5f;
        [SerializeField] UnityEvent OnStartCooking;
        public bool isCooking
        {
            get
            {
                return currentIngredientCooking != null;
                // return workingSuface.childCount > 0;
            }
        }
        Ingredient currentIngredientCooking;

        protected void Update()
        {
            HandleCooking();
        }

        public override bool Interact(Ingredient ingredient)
        {
            OnInteract.Invoke();
            
            if (isCooking)
            {
                Debug.Log("Already cooking!");
                return false;
            }
            if (!ingredient.isCookable)
            {
                Debug.Log("Can't cook this item!");
                return false;
            }

            ////COOK!
            Debug.Log("Started cooking item!");

            //1. Place item on surface and stop physics
            ingredient.transform.position = workSurface.position;
            ingredient.SetPhysicsActive(false);

            //2. Start cooking
            currentIngredientCooking = ingredient;
            OnStartCooking.Invoke();

            return true;
        }

        protected virtual void HandleCooking()
        {
            //Continue cooking any ingredients
            if (!currentIngredientCooking) return;

            // Debug.Log("Cooking " + currentIngredientCooking);
            // Debug.Log("Progress " + currentIngredientCooking.cookProgress);
            // var cookAmount = cookTime * 0.1f * Time.deltaTime;
            // Debug.Log("Cook amount: " + cookAmount);

            currentIngredientCooking.cookProgress += cookAmount;
        }
    }
}
