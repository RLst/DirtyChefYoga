using System;
using UnityEngine;
using pokoro;
using UnityEngine.Events;

namespace DirtyChefYoga
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(PlayerInput))]
    // [RequireComponent(typeof(PlayerInteract))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] bool debug = true;
        [Space]

        [Header("Move")]
        [SerializeField] float maxSpeed = 5f;

        [Header("Dash")]
        [SerializeField] float dashSpeed = 13f;
        [SerializeField] float dashDrag = 1.5f;
        private bool isDashing;
        float currentDashSpeed;

        [Header("Interaction")]
        [SerializeField] Transform handPoint;
        [SerializeField] Vector3 castExtents = new Vector3(0.3f, 1, 0.5f);
        [SerializeField] float castLength = 1.5f;
        public bool isHoldingItem
        {
            get {
                //If there is something childed to this transform
                //means we're holding something
                return currentItem != null;
                // return handPoint.childCount > 0;       
            }
        }

        public UnityEvent OnInvalidAction;

        Ingredient currentItem;

        private CharacterController controller;
        private PlayerInput input;
        Camera cam;
        private Quaternion facing;

        // private PlayerInteract interact;
        void Start()
        {
            controller = GetComponent<CharacterController>();
            input = GetComponent<PlayerInput>();
            // interact = GetComponent<PlayerInteract>();
            cam = FindObjectOfType<Camera>();
        }

        void Update()
        {
            MoveCharacter();
            if (input.interacted) HandleInteractions();
        }

        #region MOVE
        void MoveCharacter()
        {
            #region Move & Dash
            //Set basic speed
            var speedTotal = maxSpeed;

            //Calculate dash boost
            if (isDashing)   //Start reducing the dash boost (linearly) if currently dashing
            {
                currentDashSpeed -= dashDrag;
                if (currentDashSpeed < 0)
                {
                    //Stop dash status and zero out
                    isDashing = false;
                    currentDashSpeed = 0;
                }
            }
            if (input.dashed && isDashing == false)  //Can't hold the dash button
            {
                // speedMultiplier = dashSpeed;
                isDashing = true;
                currentDashSpeed = dashSpeed;
            }
            //Add dash
            speedTotal += currentDashSpeed;

            //Calculate move motion vector
            Vector3 moveMotion = input.leftAxis.x * cam.transform.RightSansYNormalized() * speedTotal +
                                input.leftAxis.y * cam.transform.ForwardSansYNormalized() * speedTotal;
            #endregion

            #region Orient in the right direction
            if (moveMotion.magnitude > 0)
            {
                facing = Quaternion.LookRotation(moveMotion);
            }

            #endregion

            //Combine all motion vectors and apply to player
            Vector3 resultantMotion = moveMotion;   // + jumpMotion;
            controller.Move(resultantMotion * Time.deltaTime);
            transform.rotation = facing;
        }
        #endregion

        #region INTERACTIONS
        private void HandleInteractions()
        {
            //If holding ingredient
            if (currentItem)
            {
                //If there's a station in front of you then interact with it using the ingredient
                // if (Physics.BoxCast(transform.position, castExtents, transform.forward, out hit, transform.rotation, castLength))
                if (DetectInteractable<Station>(out Station stationHit))
                {
                    // Debug.Log("Station found");

                    //Pass ingredient to station
                    if (stationHit.Interact(currentItem))
                    {
                        // Debug.Log("Successfully passed ingredient to station");
                        ReleaseIngredient();
                    }
                    OnInvalidAction.Invoke();
                }
                //Otherwise drop the ingredient
                else
                {
                    ReleaseIngredient();
                }
            }
            //If not holding ingredient
            else
            {
                //If ingredient found
                if (DetectInteractable<Ingredient>(out Ingredient ingredientHit))
                {
                    // Debug.Log("Ingredient found");

                    PickUpIngredient(ingredientHit);
                }
                //Do nothing if you're not holding anything
            }
        }

        private void ReleaseIngredient()
        {
            // Debug.Log("Drop the item!");

            //Unchild
            currentItem.transform.SetParent(null);

            //Make it a physics object
            currentItem.SetPhysicsActive(true);

            //RELEASE
            currentItem = null;
        }

        private void PickUpIngredient(Ingredient ingredientHit)
        {
            // Debug.Log("Picking up ingredient!");

            ////Pick it up
            //Set as picked up
            currentItem = ingredientHit;
            //Move to the hand
            currentItem.transform.SetPositionAndRotation(handPoint.transform.position, handPoint.transform.rotation);
            //Set it as a child
            currentItem.transform.SetParent(this.transform);
            //Deactivate physics
            currentItem.SetPhysicsActive(false);
        }

        //Detect object of type T according to set cast paramters
        bool DetectInteractable<T>(out T interactableFound) where T : MonoBehaviour
        {
            // Debug.Log("Detecting interactable");

            T hitComponent;
            bool isHit = Physics.BoxCast(transform.position, castExtents, transform.forward, out RaycastHit hit, Quaternion.identity, castLength);

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
        #endregion


        void OnGUI()
        {
            if (debug)
            {
                GUILayout.Label("Player Controller");
                if (currentItem != null)
                {
                    GUILayout.Label("Holding a " + currentItem.name);
                }
            }
        }

    }
}



// void HandleMovement()
// {
//     controller.Move(new Vector3(input.move * moveSpeed * Time.deltaTime, 0, 0));
// }