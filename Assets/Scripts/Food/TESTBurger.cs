using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DirtyChefYoga;

public class TESTBurger : MonoBehaviour
{
    public Burger burger;
    public GameObject topBun;
    public GameObject tomatoes;
    public GameObject lettuce;
    public GameObject cheese;
    public GameObject pattyCooked;
	public GameObject pattyUncooked;
    public GameObject bottomBun;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            var newIngredient = Instantiate(topBun, transform.position, Quaternion.identity);
            burger.AddIngredient(newIngredient.GetComponent<Ingredient>());
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            var newIngredient = Instantiate(tomatoes, transform.position, Quaternion.identity);
            burger.AddIngredient(newIngredient.GetComponent<Ingredient>());
        };
        if (Input.GetKeyDown(KeyCode.L))
        {
            var newIngredient = Instantiate(lettuce, transform.position, Quaternion.identity);
            burger.AddIngredient(newIngredient.GetComponent<Ingredient>());
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            var newIngredient = Instantiate(cheese, transform.position, Quaternion.identity);
            burger.AddIngredient(newIngredient.GetComponent<Ingredient>());
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            var newIngredient = Instantiate(pattyCooked, transform.position, Quaternion.identity);
            burger.AddIngredient(newIngredient.GetComponent<Ingredient>());
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            var newIngredient = Instantiate(pattyUncooked, transform.position, Quaternion.identity);
            burger.AddIngredient(newIngredient.GetComponent<Ingredient>());
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            var newIngredient = Instantiate(bottomBun, transform.position, Quaternion.identity);
            burger.AddIngredient(newIngredient.GetComponent<Ingredient>());
        }
    }

}
