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
		// [SerializeField] new Collider collider;
        [SerializeField] Transform handPoint;
        [SerializeField] Vector3 castHalfExtents = new Vector3(0.3f, 1, 0.5f);
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

			db();
        }

		private void db()
		{
			if (DetectInteractable<Ingredient>(out Ingredient ingredient))
			{
				Debug.Log("Detecting ingredient!: " + ingredient);
			}
			else if (DetectInteractable<Station>(out Station station))
			{
				Debug.Log("Detecting station!: " + station);
			}

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
                if (DetectInteractable<Station>(out Station stationHit))
                {
                    // Debug.Log("Station found");

                    //Pass ingredient to station
                    if (stationHit.Interact(currentItem))
                    {
                        Debug.Log("Successfully passed ingredient to station");
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
            Debug.Log("Drop the item!");

            //Unchild
            currentItem.transform.SetParent(null);

            //Make it a physics object
            currentItem.SetPhysicsActive(true);

            //RELEASE
            currentItem = null;
        }

        private void PickUpIngredient(Ingredient ingredientHit)
        {
            Debug.Log("Picking up ingredient!");

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
        bool DetectInteractable<T>(out T hit) where T : MonoBehaviour
        {

			bool isHit = Physics.BoxCast(transform.position, castHalfExtents, transform.forward, out RaycastHit hitInfo, Quaternion.LookRotation(transform.forward), castLength);
            // bool isHit = Physics.BoxCast(transform.position + castOffset, castHalfExtents, transform.forward, out RaycastHit castHit, transform.rotation);

            //If something hit
            if (isHit)
            {
				// Debug.DrawRay(transform.position, transform.forward * 10, Color.blue);
                hit = hitInfo.collider.GetComponent<T>();
                //Get found component
                if (hit is T) //item is the right type!
                {
					DrawDebugLineArray(0.25f, Color.green);
                    //set found object
                    return true;
                }
            }
            //Nothing found
            hit = null;
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

		void OnDrawGizmos() 
		{
			Gizmos.color = Color.red;
			Transform t = transform;
			for (float i = -castHalfExtents.y; i < castHalfExtents.y; i += 0.25f)
			{
				Vector3 leftFrom = t.position + t.up * i - t.right * castHalfExtents.x;
				Vector3 rightFrom = t.position + t.up * i + t.right * castHalfExtents.x;
				Vector3 leftTo = t.position + t.up * i - t.right * castHalfExtents.x + t.forward * castLength;
				Vector3 rightTo = t.position + t.up * i + t.right * castHalfExtents.x + t.forward * castLength;
				Gizmos.DrawLine(leftFrom, leftTo);
				Gizmos.DrawLine(rightFrom, rightTo);
			}
		}

		void DrawDebugLineArray(float arraySpacing, Color color)
		{
			for (float i = -castHalfExtents.y; i < castHalfExtents.y; i += arraySpacing)
			{
				for (float j = -castHalfExtents.x; j < castHalfExtents.x; j += arraySpacing)
				{
					var t = transform;
					var from = t.position + t.up*i + t.right*j;
					var to = t.position + t.up*i + t.right*j + t.forward*castLength;
					Debug.DrawLine(from, to, color);
				}
			}
		}

    }
}



// void HandleMovement()
// {
//     controller.Move(new Vector3(input.move * moveSpeed * Time.deltaTime, 0, 0));
// }