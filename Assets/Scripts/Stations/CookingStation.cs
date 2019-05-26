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
		bool isCooked = false;
		bool isOvercooked = false;

        protected virtual void Update()
        {
            HandleCooking();
        }

        public override bool Insert(Ingredient item)
        {
            //Already cooking
            if (isCooking) {
                Debug.Log("Already cooking!");
                return false;
            }
            //Can item be cooked?
            if (!item.isCookable) {
                Debug.Log("Can't cook this item!");
                return false;
            }

            ///Start cooking!
			SetCurrentItem(item); 	//Setting as current item will start cooking it

			ResetCookStatus();

			//Successful insert
            OnInserted.Invoke();
            return true;
        }

		private void ResetCookStatus()
		{
			isCooked = false;
			isOvercooked = false;
		}

		public override bool Remove(out Ingredient @out)
		{
			//If there is something cooking, then release it and stop cooking
			if (isCooking)
			{
				@out = currentItem;

				//Stop cooking
				ReleaseCurrentItem();

				//Successful remove
				OnRemoved.Invoke();
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
            if (currentItem.cookProgress > 1.0f && !isCooked)
            {
				isCooked = true;
                OnCooked.Invoke();
            }	
            else if (currentItem.cookProgress > 2.0f && !isOvercooked)
            {
				isOvercooked = true;
                OnOvercooked.Invoke();
            }
        }
    }
}
