﻿using UnityEngine;
namespace DirtyChefYoga
{
    public class Benchtop : Station
    {
        public override bool Insert(Ingredient item)
        {
            //Can't place on top if there's already another item
            if (currentItem) return false;

			//Set the current item
			SetCurrentItem(item);

			//Successful push
            OnInserted.Invoke();
            return true;
        }

        public override bool Remove(out Ingredient @out)
        {
            //If there's no ingredient, reject call
            if (!currentItem)
            {
                @out = null;
                return false;
            }

			//Otherwise ALWAYS give ingredient
            @out = currentItem;

            ReleaseCurrentItem();
			
			//Successful pop
			OnRemoved.Invoke();
            return true;
        }
    }
}
