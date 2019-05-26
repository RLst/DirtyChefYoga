using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DirtyChefYoga
{
    public class Fryer : CookingStation
    {
        [SerializeField] GameObject cookedFriesPrefab;

        public override bool Insert(Ingredient item)
        {
            //Only pototoes can be cooked
            if (item is Potato)
            {
                //Start cooking as usual
                return base.Insert(item);
            }

			//Rejected
            return false;
        }

        protected override void HandleCooking()
        {
			//Cook as usual
            base.HandleCooking();

			//Potato once cooked need to be swapped over for fries
            if (currentItem.cookProgress > 1.0f)
            {
                OnCooked.Invoke();
                
                //Swap out potato for fries
                var cookedFries = Instantiate(cookedFriesPrefab, workSurface.position, workSurface.rotation);
				Destroy(currentItem.gameObject);
                
				//Set as the current item
				currentItem = cookedFries.GetComponent<Ingredient>();
            }
        }
    }
}