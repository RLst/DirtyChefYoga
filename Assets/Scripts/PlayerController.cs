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

        private CharacterController controller;
        private PlayerInput input;
        Camera cam;
        private Quaternion facing;


        void Start()
        {
            controller = GetComponent<CharacterController>();
            input = GetComponent<PlayerInput>();
            cam = FindObjectOfType<Camera>();
        }

        void Update()
        {
            MoveCharacter();
        }

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

        void OnGUI()
        {
            if (debug)
            {
                GUILayout.Label("Player Controller");
				GUILayout.Space(5);
                GUILayout.Label("isDashing: " + isDashing);
            }
        }

    }
}



// void HandleMovement()
// {
//     controller.Move(new Vector3(input.move * moveSpeed * Time.deltaTime, 0, 0));
// }