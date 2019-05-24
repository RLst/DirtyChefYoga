using System;
using UnityEngine;

namespace DirtyChefYoga
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(PlayerInput))]
    // [RequireComponent(typeof(PlayerInteract))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] bool debug = true;
        [Space]

        [SerializeField] float moveSpeed = 5f;
        [Space]
        [SerializeField] Transform handPoint;
        [SerializeField] Vector3 castExtents = new Vector3(0.3f, 1, 0.5f);
        [SerializeField] float castLength = 1.5f;
        Ingredient itemInHand;

        private CharacterController controller;
        private PlayerInput input;
        // private PlayerInteract interact;
        void Start()
        {
            controller = GetComponent<CharacterController>();
            input = GetComponent<PlayerInput>();
            // interact = GetComponent<PlayerInteract>();
        }

        void Update()
        {
            HandleMovement();
            if (input.interacted) HandleInteractions();
        }

        private void HandleInteractions()
        {
            //If not holding ingredient
            if (itemInHand == null)
            {
                //If ingredient found
                if (DetectInteractable<Ingredient>(out Ingredient ingredientHit))
                {
                    Debug.Log("Ingredient found");

                    PickUpIngredient(ingredientHit);
                }
                //Do nothing if you're not holding anything
            }
            //If holding ingredient
            else    
            {
                //If there's a station in front of you then interact with it using the ingredient
                // if (Physics.BoxCast(transform.position, castExtents, transform.forward, out hit, transform.rotation, castLength))
                if (DetectInteractable<Station>(out Station stationHit))
                {
                    Debug.Log("Station found");

                    //Try using interactable on station
                    if (stationHit.Interact(itemInHand))
                    {
                        Debug.Log("Successful station interaction!");

                        //The station now has control of the ingredient, so release it
                        itemInHand = null;
                    }
                }
                //Otherwise drop the ingredient
                else
                {

                    DropIngredient();
                }
            }
        }

        private void DropIngredient()
        {
            Debug.Log("Drop the item!");
            
            //Unchild
            itemInHand.transform.SetParent(null);

            //Make it a physics object
            itemInHand.SetPhysicsActive(true);

            //RELEASE
            itemInHand = null;
        }

        private void PickUpIngredient(Ingredient ingredientHit)
        {
            Debug.Log("Picking up ingredient!");

            ////Pick it up
            //Set as picked up
            itemInHand = ingredientHit;
            //Move to the hand
            itemInHand.transform.SetPositionAndRotation(handPoint.transform.position, handPoint.transform.rotation);
            //Set it as a child
            itemInHand.transform.SetParent(this.transform);
            //Deactivate physics
            itemInHand.SetPhysicsActive(false);
        }

        //Detect object of type T according to set cast paramters
        bool DetectInteractable<T>(out T interactableFound) where T : MonoBehaviour
        {
            Debug.Log("Detecting interactable");

            T hitComponent;
            bool isHit = Physics.BoxCast(transform.position, castExtents, transform.forward, out RaycastHit hit, transform.rotation, castLength);

            //If something hit
            if (isHit)
            {
                hitComponent = hit.collider.GetComponent<T>();
                //Get found component
                if (hitComponent is T) //item is the right type!
                {
                    //set found object
                    interactableFound = hitComponent;
                    return true;
                }
            }
            //Nothing found
            interactableFound = null;
            return false;
        }

        void HandleMovement()
        {
            controller.Move(new Vector3(input.move * moveSpeed * Time.deltaTime, 0, 0));
        }

        void OnGUI()
        {
            if (debug)
            {
                GUILayout.Label("Player Controller");
                if (itemInHand != null)
                {
                    GUILayout.Label("Holding a " + itemInHand.name);
                }
            }
        }

    }
}