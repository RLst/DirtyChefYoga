using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DirtyChefYoga
{
	[SelectionBase]
    public class Fryer : CookingStation
    {
        [SerializeField] GameObject cookedFriesPrefab;

		void Start()
		{
			//Callback for when the potatoes are cooked
			OnCooked.AddListener(SwapPotatoForCookedFries);
		}

        public override bool InsertItem(Ingredient item)
        {
            //FILTER: Only pototoes can be cooked
            if (item is Potato)
            {
                return base.InsertItem(item);
            }
			//And can also retake cooked fries in the event the player accidentally put it back in
			else if (item is Fries)
			{
				return base.InsertItem(item);
			}

			//Rejected
            return false;
        }

		void SwapPotatoForCookedFries()
		{
			//Make sure it's a potato!
			if (currentItem is Potato)
			{
				//Swap out potato for fries
				var cookedFries = Instantiate(cookedFriesPrefab, anchor.position, anchor.rotation);
				Destroy(currentItem.gameObject);

				//Set current item and set the correct cook amount
				currentItem = cookedFries.GetComponent<Ingredient>();
				currentItem.cookAmount = 1f;
			}
		}
    }
}