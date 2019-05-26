using UnityEngine;
namespace DirtyChefYoga
{
    public class Bin : Station
    {
        [SerializeField] float destroyDelay = 2f;

        public override bool Insert(Ingredient item) {
            OnInsert.Invoke();

            ///Bin can always take items

            //Position the ingredient and let it drop
			item.transform.position = workSurface.position;

			//No need to set it as the current item
			
			//Destroy the item
            Destroy(item.gameObject, destroyDelay);

            return true;
        }

		public override bool Remove(out Ingredient item)
		{
			//Once you chuck something in the bin it CANNOT be removed! (Inaccessible sire!)
			item = null;
			return false;
		}
    }
}
