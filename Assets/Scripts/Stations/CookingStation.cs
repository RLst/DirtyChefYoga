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
        [SerializeField] protected UnityEvent OnCooked, OnOvercooked;
        public bool isCooking
        {
            get
            {
                return currentItem != null;
            }
        }

        protected virtual void Update()
        {
            HandleCooking();
        }

        public override bool Insert(Ingredient item)
        {
            //Already cooking
            if (isCooking)
            {
                Debug.Log("Already cooking!");
                return false;
            }
            //Can item be cooked?
            if (!item.isCookable)
            {
                Debug.Log("Can't cook this item!");
                return false;
            }

            //Start cooking!
            OnInteract.Invoke();

            //Place item on surface and stop physics
            item.transform.position = workSurface.position;
            item.SetPhysicsActive(false);

        	//Start cooking
            currentItem = item;

            return true;
        }

		public override bool Remove(out Ingredient @out)
		{
			//If there is something cooking, then release it and stop cooking
			if (isCooking)
			{
				//Release
				@out = currentItem;
				OnRemoved.Invoke();

				//Stop cooking
				currentItem = null;
				return true;
			}
			//Otherwise nothing to take
			@out = null;
			return false;
		}

        protected virtual void HandleCooking()
        {
            //Continue cooking any ingredients
            if (!currentItem) return;

            //Do cooking
            currentItem.cookProgress += cookAmount;

            //Invoke events
            if (currentItem.cookProgress > 1.0f)
            {
                OnCooked.Invoke();
            }	
            else if (currentItem.cookProgress > 2.0f)
            {
                OnOvercooked.Invoke();
            }
        }
    }
}
