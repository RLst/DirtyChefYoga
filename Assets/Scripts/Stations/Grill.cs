using UnityEngine;
namespace DirtyChefYoga
{
    public class Grill : CookingStation
    {
        public override bool Insert(Ingredient item)
        {
			//FILTER: Only accept patties
            if (item is Patty)
            {
                return base.Insert(item);
            }

			//Rejected
            return false;
        }
    }
}
