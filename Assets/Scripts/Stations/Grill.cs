using UnityEngine;
namespace DirtyChefYoga
{
	[SelectionBase]
    public class Grill : CookingStation
    {
        public override bool InsertItem(Ingredient item)
        {
			//FILTER: Only accept patties
            if (item is Patty)
            {
                return base.InsertItem(item);
            }

			//Rejected
            return false;
        }
    }
}
