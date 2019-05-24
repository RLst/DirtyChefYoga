using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DirtyChefYoga
{
    public abstract class CookingStation : Station
    {
        [SerializeField] protected float cookAmount = 0.001f;
        // [SerializeField] float cookTime = 5f;
        [SerializeField] protected UnityEvent OnFinishedCooking, OnOvercooked;
        public bool isCooking
        {
            get
            {
                return currentIngredientCooking != null;
                // return workingSuface.childCount > 0;
            }
        }
        protected Ingredient currentIngredientCooking;

        protected virtual void Update()
        {
            HandleCooking();
        }

        public override bool Interact(Ingredient ingredient)
        {
            
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
            OnInteract.Invoke();	//OnStartCooking

            return true;
        }

        protected virtual void HandleCooking()
        {
            //Continue cooking any ingredients
            if (!currentIngredientCooking) return;

            //Do cooking
            currentIngredientCooking.cookProgress += cookAmount;

            //Invoke events
            if (currentIngredientCooking.cookProgress > 1.0f)
            {
                OnFinishedCooking.Invoke();
            }
            else if (currentIngredientCooking.cookProgress > 2.0f)
            {
                OnOvercooked.Invoke();
            }

        }
    }
}
