using UnityEngine;
namespace DirtyChefYoga
{
	[SelectionBase]
    public class Bin : Station
    {
        [SerializeField] float destroyDelay = 2f;

        public override bool InsertItem(Ingredient item) {
			//Since a bin can always take items, no need to set it as the current item

			//Disown item 
			item.transform.SetParent(null);

            //Position the ingredient and let it drop
			item.transform.position = anchor.position;
			item.SetPhysicsActive(true);
			
			//Destroy the item
            Destroy(item.gameObject, destroyDelay);

			//Successful "insert"
            OnInserted.Invoke();
            return true;
        }

		public override bool RemoveItem(out Ingredient @out)
		{
			//Once you chuck something in the bin it CANNOT be removed! (Inaccessible sire!)
			@out = null;
			return false;
		}
    }
}
