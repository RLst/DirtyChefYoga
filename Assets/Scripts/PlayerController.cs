using System;
using UnityEngine;

namespace DirtyChefYoga
{
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float moveSpeed = 5f;
        [Space]
        [SerializeField] Transform handPoint;
        // [SerializeField] Transform rightHandPos;
        [SerializeField] Vector3 castExtents = new Vector3(0.3f, 1, 0.5f);
        [SerializeField] float castLength = 1.5f;
        Ingredient itemInHand;

        private CharacterController controller;
        private PlayerInput input;
        void Start()
        {
            controller = GetComponent<CharacterController>();
            input = GetComponent<PlayerInput>();
        }

        void Update()
        {
            HandleMovement();
            if (input.interacted) HandleInteractions();
        }

        private void HandleInteractions()
        {
            RaycastHit hit;
            //If holding something
            if (itemInHand)
            {
                //If there's a station in front of you
                if (Physics.BoxCast(transform.position, castExtents, transform.forward, out hit, transform.rotation, castLength))
                {
                    var stationHit = hit.collider.GetComponent<Station>();
                    if (stationHit)
                    {
                        //Try using interactable on station
                        if (stationHit.Interact(itemInHand))
                            Debug.Log("Successful station interaction!");
                    }
                }
                //Theres nothing in front of you
                else
                {
                    //Drop the item
                    itemInHand.transform.SetParent(null);

                    //Make it a physics object
                    itemInHand.SetPhysics(true);
                }
            }
            //If not holding something
            else
            {
                //If there's an interactable in front of you
                if (Physics.BoxCast(transform.position, castExtents, transform.forward, out hit, transform.rotation, castLength))
                {
                    //And it's a valid item
                    var ingHit = hit.collider.GetComponent<Ingredient>();
                    if (ingHit != null)
                    {
                        ////Pick it up
                        //Move and child to hand and deactivate physics (ie. turn off )
                        //Move to the hand
                        ingHit.transform.SetPositionAndRotation(handPoint.transform.position, handPoint.transform.rotation);
                        //Set it as a child
                        ingHit.transform.SetParent(this.transform);
                        //Deactivate physics
                        ingHit.SetPhysics(false);
                    }
                }
            }
        }

        void HandleMovement()
        {
            controller.Move(new Vector3(input.move * moveSpeed * Time.deltaTime, 0, 0));
        }

    }
}