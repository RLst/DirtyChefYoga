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

        [Header("Left Axis")]
        [SerializeField] XboxAxis leftAxisX = XboxAxis.LeftStickX;
        [SerializeField] XboxAxis leftAxisY = XboxAxis.LeftStickY;
        public Vector2 leftAxis
        {
            get
            {
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

        [Header("Right Axis")]
        [SerializeField] XboxAxis rightAxisX = XboxAxis.RightStickX;
        [SerializeField] XboxAxis rightAxisY = XboxAxis.LeftStickY;
        public Vector2 rightAxis
        {
            get
            {
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

        [Header("Triggers")]
        [SerializeField] XboxAxis leftTriggerAxis = XboxAxis.LeftTrigger;
        [SerializeField] XboxAxis rightTriggerAxis = XboxAxis.RightTrigger;
        public float move
        {
            get
            {
                if (useRaw)
                {
                    return XCI.GetAxisRaw(rightTriggerAxis) - XCI.GetAxisRaw(leftTriggerAxis);
                }
                else
                {
                    return XCI.GetAxis(rightTriggerAxis) - XCI.GetAxis(leftTriggerAxis);
                }
            }
        }

        //Buttons
        [SerializeField] XboxButton interactButton = XboxButton.A;
        public bool interacting { get { return XCI.GetButton(interactButton); } }
        public bool interacted { get { return XCI.GetButtonDown(interactButton); } }
        // [SerializeField] XboxButton pickupButton = XboxButton.X;
        // public bool pickingUp { get { return  XCI.GetButton(pickupButton); } }
        // public bool pickedUp { get { return  XCI.GetButtonDown(pickupButton); } }
        [SerializeField] XboxButton dashButton = XboxButton.B;
        public bool dashing { get { return XCI.GetButton(dashButton); } }
        public bool dashed { get { return XCI.GetButtonDown(dashButton); } }

        void OnGUI()
        {
            if (debug)
            {
                GUILayout.Label("Player Input");
                if (interacting) GUILayout.Label("Interacting!");
                if (interacted) GUILayout.Label("Interacted!");
                // if (pickingUp) GUILayout.Label("Picking Up!");
                // if (pickedUp) GUILayout.Label("Picked Up!");
                if (dashing) GUILayout.Label("Dashing!");
                if (dashed) GUILayout.Label("Dashed!");
                GUILayout.Label("LeftAxis: " + leftAxis.x + ", " + leftAxis.y);
                GUILayout.Label("RightAxis: " + rightAxis.x + ", " + rightAxis.y);
            }
        }

    }
}