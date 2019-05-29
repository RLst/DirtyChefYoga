using UnityEngine;
namespace DirtyChefYoga
{
	[SelectionBase]
    public class Benchtop : Station
    {
        public override bool InsertItem(Ingredient item)
        {
            //Can't place on top if there's already another item
            if (currentItem) return false;

			//Set the current item
			SetCurrentItem(item);

			//Successful push
            OnInserted.Invoke();
            return true;
        }

        public override bool RemoveItem(out Ingredient @out)
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
