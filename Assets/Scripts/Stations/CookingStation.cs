using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DirtyChefYoga
{
    public abstract class CookingStation : Station
    {
		[SerializeField] bool debug;
		[Space]

        [SerializeField] protected float cookingPower = 0.001f;		//How fast this station can cook
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
			SetCurrentItem(item); 	//Setting a current item will start cooking it

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
            currentItem.cookAmount += cookingPower;

            //Invoke events

			//COOKED
            if (currentItem.cookAmount > (int)CookStatus.Cooked && !isCooked)
            {
				isCooked = true;
                OnCooked.Invoke();
            }	
			//OVERCOOKED
            else if (currentItem.cookAmount > (int)CookStatus.OverCooked && !isOvercooked)
            {
				isOvercooked = true;
                OnOvercooked.Invoke();
            }
        }

		void OnGUI()
		{
			if (debug)
			{
				GUILayout.Label("Cooking Station of type: " + this.GetType().ToString());
				GUILayout.Space(5);

				if (!currentItem) return;
					GUILayout.Label("Item cook amount: " + currentItem.cookAmount);
					GUILayout.Label("Item cook status:  " + currentItem.cookStatus);
			}
		}
    }
}
