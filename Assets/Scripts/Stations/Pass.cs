using UnityEngine;
using UnityEngine.Events;

namespace DirtyChefYoga
{
	[System.Serializable]
	public class DCYOrderEvent : UnityEvent<Order> { }

	//A pass should always submit the order upon interact
	public class Pass : Station
	{
		[SerializeField] bool debug = true;
		[Space]
		[SerializeField] int newOrderLayer;
		Order currentOrder = null;
		[SerializeField] DCYOrderEvent OnSubmitOrder;


		public override bool Insert(Ingredient @in)
		{
			//If there's currently no order on the pass
			if (!currentOrder)
			{
				//CREATE NEW BURGER ORDER
				if (@in is BurgerIngredient)
				{
					if (@in is BottomBun)
					{
						currentOrder = CreateNewOrder<Burger>();
						if (currentOrder.AddIngredient(@in))
						{
							Debug.Log("Successfully started a burger order");
						}

						return true;
					}
				}

				//CREATE AND SUBMIT NEW FRENCH FRIES ORDER
				else if (@in is Fries)
				{
					currentOrder = CreateNewOrder<FrenchFries>();
					if (currentOrder.AddIngredient(@in))
					{
						Debug.Log("Successfully started a fries order");
					}
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
					if (@in is BurgerIngredient)
					{
						currentOrder.AddIngredient(@in);

						//IF WAS TOP BUN THEN SUBMIT!
						if (@in is TopBun)
						{
							SubmitOrder(currentOrder);
						}

						return true;
					}
				}

				//Else
				Debug.LogWarning("Invalid ingredient input!");
				return false;
			}
		}

		public override bool Remove(out Ingredient @out)
		{
			Debug.LogWarning("Cannot remove anything from pass!");

			@out = null;
			return false;
		}

		//One way submit to the ticket system for approval + clean up
		void SubmitOrder(Order order)
		{
			//Submit the order
			OnSubmitOrder.Invoke(order);

			ReleaseOrder();
		}

		//Clean up
		private void ReleaseOrder()
		{
			//Delete the object!
			Destroy(currentOrder.gameObject, 0.2f);

			//Unset current order
			currentOrder = null;
		}

		// //Since the pass is a special station that doesn't use "currentItem"
		// //It needs a special function so that items don't keep sticking to the player
		// void TakeCurrentItem(Ingredient item)
		// {
		// 	currentItem = null;		//Current item should not point to anything
		// 	item.transform.SetParent(null);		//Disown item

		// }

		//----- Utilities -----
		private T CreateNewOrder<T>() where T : Order
		{
			GameObject newOrderObject = new GameObject("Burger");
			newOrderObject.transform.SetPositionAndRotation(anchor.position, anchor.rotation);
			newOrderObject.layer = newOrderLayer;
			T newOrder = newOrderObject.AddComponent<T>();
			return newOrder;
		}

		//----- Debug -----
		void OnGUI()
		{
			GUILayout.Label("Pass");
			GUILayout.Space(5);
			GUILayout.Label("Current Order: " + currentOrder);
		}
	}
}


// newBurgerObject.transform.SetPositionAndRotation(workSurface.position, workSurface.rotation);
// 						newBurgerObject.layer = newOrderLayer;
// 						var newBurger = newBurgerObject.AddComponent<Burger>();
// ingredient.SetPhysicsActive(false);
// 						ingredient.transform.position = newBurger.transform.position + Vector3.up* newBurger.currentThickness;
// ingredient.transform.SetParent(newBurger.transform);
// 						newBurger.currentThickness += (ingredient as BurgerIngredient).thickness;
// 						newBurger.ingredients.Add(ingredient);

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
