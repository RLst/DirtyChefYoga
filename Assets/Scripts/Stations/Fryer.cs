using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DirtyChefYoga
{
    public class Fryer : CookingStation
    {
        [SerializeField] GameObject cookedFriesPrefab;

		void Start()
		{
			//Listen for OnCooked event
			OnCooked.AddListener(SwapPotatoForFries);
		}

        public override bool Insert(Ingredient item)
        {
            //FILTER: Only pototoes can be cooked
            if (item is Potato)
            {
                //Start cooking as usual
                return base.Insert(item);
            }

			//Rejected
            return false;
        }

		void SwapPotatoForFries()
		{
			//Swap out potato for fries
			var cookedFries = Instantiate(cookedFriesPrefab, anchor.position, anchor.rotation);
			Destroy(currentItem.gameObject);
			//Set as the current item
			currentItem = cookedFries.GetComponent<Ingredient>();
		}
    }
}