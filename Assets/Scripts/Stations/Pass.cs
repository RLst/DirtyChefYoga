using UnityEngine;
using UnityEngine.Events;

namespace DirtyChefYoga
{
	//A pass should always submit the order upon interact
	public class Pass : Station
	{
		/* Brainstorm:
		- Upon insert:
			- if the item passed in is a bottom bun
				- create/instantiate a empty burger object and start stacking
			- if the item passed in is a top bun
				- try submit
					- if the submit is invalid then dump the food object
			- if the item passed in are chips
				- create/instantiate a empty frenchFries object
			- don't except anything else
		*/

		[SerializeField] TicketSystem ticketSystem;
		Order currentOrder;

		UnityEvent OnOrderSubmit;

		public override bool Insert(Ingredient item)
		{
			//If there's currently no order on the pass
			if (!currentItem)
			{
				//CREATE NEW BURGER ORDER
				if (item is BurgerIngredient)
				{
					if (item is BottomBun) {
						currentOrder = CreateNewOrder<Burger>();
						currentOrder.AddIngredient(item);
						return true;
					}
				}
				//CREATE AND SUBMIT NEW FRENCH FRIES ORDER
				else if (item is Fries)
				{
					currentOrder = CreateNewOrder<FrenchFries>();
					SubmitOrder(currentOrder);
					return true;
				}

				//Else
				Debug.LogWarning("Invalid ingredient input!");
				return false;
			}
			//Else an order already exists
			else
			{
				//ADD TO BURGER STACK
				if (currentOrder is Burger)
				{
					currentOrder.AddIngredient(item);

					//IF TOP BUN THEN SUBMIT!
					if (item is TopBun) {
						SubmitOrder(currentOrder);
					}
					return true;
				}
				
				//Else
				Debug.LogWarning("Invalid ingredient input!");
				return false;
			}
		}

		public override bool Remove(out Ingredient @out)
		{
			//Cannot remove anything from the pass!
			Debug.LogWarning("Cannot remove item from pass!");
			@out = null;
			return false;
		}

		void SubmitOrder(Order order)
		{
			//Submit the order
			ticketSystem.CheckTicket(order);
			OnOrderSubmit.Invoke();

			//Reset current order
			currentOrder = null;
		}

		//----- Utilities -----
		private T CreateNewOrder<T>() where T : Order
		{
			GameObject limbo = Instantiate(new GameObject(), workSurface.position, workSurface.rotation);
			return limbo.AddComponent<T>();
		}
	}
}