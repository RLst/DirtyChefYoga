using UnityEngine;
using UnityEngine.Events;

namespace DirtyChefYoga
{

	[System.Serializable]
	public class DCYOrderEvent : UnityEvent<Order> { }

	//Used to assemble and submit orders
	[SelectionBase]
	public class Pass : Station
	{
		[SerializeField] bool debug = true;
		[Space]

		[Header("Order")]
		[SerializeField] int newOrderLayer = 9;
		[SerializeField] float submitDelay = 0.3f;
		[SerializeField] DCYOrderEvent OnSubmitOrder;
		Order currentOrder = null;


		public override bool InsertItem(Ingredient @in)
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

		public override bool RemoveItem(out Ingredient @out)
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
			Destroy(currentOrder.gameObject, submitDelay);

			//Unset current order
			currentOrder = null;
		}

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
			if (debug)
			{
				GUILayout.Label("Pass");
				GUILayout.Space(5);
				GUILayout.Label("Current Order: " + currentOrder);
			}
		}
	}
}

