using UnityEngine;
namespace DirtyChefYoga
{
    public class Benchtop : Station
    {
        public override bool Insert(Ingredient item)
        {
            OnInteract.Invoke();

            //Can't place on top if there's already another item
            if (currentItem) return false;

			//Set the current item
			currentItem = item;

            //Place on the work surface
            item.transform.position = workSurface.position;

            //Turn off physics
            item.SetPhysicsActive(false);

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
			OnRemoved.Invoke();

			//Unset current ingredient
            currentItem = null;
            return true;
        }
    }
}
