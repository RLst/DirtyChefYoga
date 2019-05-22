using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DirtyChefYoga;

public class UTBurger : MonoBehaviour
{
	[SerializeField] bool debug = true;
	[Space]
	public Burger burger;
	public GameObject topBun;
	public GameObject tomatoes;
	public GameObject lettuce;
	public GameObject cheese;
	public GameObject pattyCooked;
	public GameObject pattyUncooked;
	public GameObject bottomBun;

	void OnGUI()
	{
		if (debug)
		{

			if (GUILayout.Button("Top Bun"))
			{
				var newIngredient = Instantiate(topBun, transform.position, Quaternion.identity);
				newIngredient.GetComponent<Rigidbody>().isKinematic = true;
				burger.AddIngredient(newIngredient.GetComponent<Ingredient>());
			}

			if (GUILayout.Button("Tomatoes"))
			{
				var newIngredient = Instantiate(tomatoes, transform.position, Quaternion.identity);
				newIngredient.GetComponent<Rigidbody>().isKinematic = true;
				burger.AddIngredient(newIngredient.GetComponent<Ingredient>());
			}
			if (GUILayout.Button("Lettuce"))
			{
				var newIngredient = Instantiate(lettuce, transform.position, Quaternion.identity);
				newIngredient.GetComponent<Rigidbody>().isKinematic = true;
				burger.AddIngredient(newIngredient.GetComponent<Ingredient>());
			}
			if (GUILayout.Button("Cheese"))
			{
				var newIngredient = Instantiate(cheese, transform.position, Quaternion.identity);
				newIngredient.GetComponent<Rigidbody>().isKinematic = true;
				burger.AddIngredient(newIngredient.GetComponent<Ingredient>());
			}
			if (GUILayout.Button("Cooked Patty"))
			{
				var newIngredient = Instantiate(pattyCooked, transform.position, Quaternion.identity);
				newIngredient.GetComponent<Rigidbody>().isKinematic = true;
				burger.AddIngredient(newIngredient.GetComponent<Ingredient>());
			}
			if (GUILayout.Button("Uncooked Patty"))
			{
				var newIngredient = Instantiate(pattyUncooked, transform.position, Quaternion.identity);
				newIngredient.GetComponent<Rigidbody>().isKinematic = true;
				burger.AddIngredient(newIngredient.GetComponent<Ingredient>());
			}
			if (GUILayout.Button("Botton Bun"))
			{
				var newIngredient = Instantiate(bottomBun, transform.position, Quaternion.identity);
				newIngredient.GetComponent<Rigidbody>().isKinematic = true;
				burger.AddIngredient(newIngredient.GetComponent<Ingredient>());
			}
		}
	}

}
