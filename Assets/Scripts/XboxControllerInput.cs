using UnityEngine;
using XboxCtrlrInput;
namespace DirtyChefYoga
{
    //Basic player input
    public class XboxControllerInput : PlayerInput
    {
        [Header("Left Axis")]
        [SerializeField] XboxAxis leftAxisX = XboxAxis.LeftStickX;
        [SerializeField] XboxAxis leftAxisY = XboxAxis.LeftStickY;
        public override Vector2 leftAxis
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
        public override Vector2 rightAxis
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
        public override float move
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
        [SerializeField] XboxButton useButton = XboxButton.X;
        public override bool @using => XCI.GetButton(useButton);
        public override bool used => XCI.GetButtonDown(useButton);
        [SerializeField] XboxButton pickupButton = XboxButton.A;
        public override bool pickingUp => XCI.GetButton(pickupButton);
        public override bool pickedUp => XCI.GetButtonDown(pickupButton);
        [SerializeField] XboxButton dashButton = XboxButton.B;
        public override bool dashing => XCI.GetButton(dashButton);
        public override bool dashed => XCI.GetButtonDown(dashButton);
    }
}