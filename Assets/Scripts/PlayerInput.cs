using UnityEngine;
using XboxCtrlrInput;

namespace DirtyChefYoga
{
    //Basic player input
    public class PlayerInput : MonoBehaviour
    {        
        [SerializeField] bool debug = true;
        [Space]
        
        [Header("Controller Settings")]
        [SerializeField] bool useRaw = false;
        [SerializeField] bool invertXaxis = false;
        [SerializeField] bool invertYaxis = false;

        [Header("Left Arm")]
        [SerializeField] XboxAxis leftAxisX = XboxAxis.LeftStickX;
        [SerializeField] XboxAxis leftAxisY = XboxAxis.LeftStickY;
        public Vector2 leftArm { 
            get {
                Vector2 result;
                //Raw if needed
                if (useRaw)
                {
                    result.x = XCI.GetAxisRaw(leftAxisX);
                    result.y = XCI.GetAxisRaw(leftAxisY);
                }
                else
                {
                    result.x = XCI.GetAxis(leftAxisX);
                    result.y = XCI.GetAxis(leftAxisY);
                }

                //Inverse if needed
                if (invertXaxis)
                    result.x = -result.x;

                if (invertYaxis)
                    result.y = -result.y;
                
                return result;
            } 
        }

        [Header("Right Arm")]
        [SerializeField] XboxAxis rightAxisX = XboxAxis.RightStickX;
        [SerializeField] XboxAxis rightAxisY = XboxAxis.LeftStickY;
        public Vector2 rightArm { 
            get {
                Vector2 result;
                //Raw if needed
                if (useRaw)
                {
                    result.x = XCI.GetAxisRaw(rightAxisX);
                    result.y = XCI.GetAxisRaw(rightAxisY);
                }
                else
                {
                    result.x = XCI.GetAxis(rightAxisX);
                    result.y = XCI.GetAxis(rightAxisY);
                }

                //Inverse if needed
                if (invertXaxis)
                    result.x = -result.x;

                if (invertYaxis)
                    result.y = -result.y;
                
                return result;
            } 
        }

        [Header("Move")]
        [SerializeField] XboxAxis moveLeftAxis = XboxAxis.LeftTrigger;
        [SerializeField] XboxAxis moveRightAxis = XboxAxis.RightTrigger;
        public float move {
            get {
                if (useRaw)
                {
                    return XCI.GetAxisRaw(moveRightAxis) - XCI.GetAxisRaw(moveLeftAxis);
                }
                else
                {
                    return XCI.GetAxis(moveRightAxis) - XCI.GetAxis(moveLeftAxis);
                }
            }
        }

        //Buttons
        [SerializeField] XboxButton interactButton = XboxButton.A;
        public bool interacting { get { return  XCI.GetButton(interactButton); } }
        public bool interacted { get { return  XCI.GetButtonDown(interactButton); } }
        [SerializeField] XboxButton pickedUpButton = XboxButton.X;
        public bool pickingUp { get { return  XCI.GetButton(pickedUpButton); } }
        public bool pickedUp { get { return  XCI.GetButtonDown(pickedUpButton); } }

        void OnGUI()
        {
            if (debug)
            {
                if (interacting) GUILayout.TextField("Interacting!");
                if (interacted) GUILayout.TextField("Interacted!");
                if (pickingUp) GUILayout.TextField("Picking Up!");
                if (pickedUp) GUILayout.TextField("Picked Up!");
                GUILayout.TextArea("Movement: " + move);
            }
        }

    }
}