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
    public GameObject patty;
    public GameObject bottomBun;
    // public Ingredient topBun;
    // public Ingredient tomatoes;
    // public Ingredient lettuce;
    // public Ingredient cheese;
    // public Ingredient patty;
    // public Ingredient bottomBun;

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
            var newIngredient = Instantiate(patty, transform.position, Quaternion.identity);
            burger.AddIngredient(newIngredient.GetComponent<Ingredient>());
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            var newIngredient = Instantiate(bottomBun, transform.position, Quaternion.identity);
            burger.AddIngredient(newIngredient.GetComponent<Ingredient>());
        }
    }

}
