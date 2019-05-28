using UnityEngine;
using XboxCtrlrInput;
namespace DirtyChefYoga
{
    //Basic player input
    public class XboxControllerInput : PlayerInput
    {
		[Header("Controller")]
		[SerializeField] XboxController controller = XboxController.Any;

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
                    result.x = XCI.GetAxisRaw(leftAxisX, controller);
                    result.y = XCI.GetAxisRaw(leftAxisY, controller);
                }
                else
                {
                    result.x = XCI.GetAxis(leftAxisX, controller);
                    result.y = XCI.GetAxis(leftAxisY, controller);
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
                    result.x = XCI.GetAxisRaw(rightAxisX, controller);
                    result.y = XCI.GetAxisRaw(rightAxisY, controller);
                }
                else
                {
                    result.x = XCI.GetAxis(rightAxisX, controller);
                    result.y = XCI.GetAxis(rightAxisY, controller);
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
                    return XCI.GetAxisRaw(rightTriggerAxis, controller) - XCI.GetAxisRaw(leftTriggerAxis, controller);
                }
                else
                {
                    return XCI.GetAxis(rightTriggerAxis, controller) - XCI.GetAxis(leftTriggerAxis, controller);
                }
            }
        }

		[Header("Actions")]
        [SerializeField] XboxButton useButton = XboxButton.X;
        public override bool @using => XCI.GetButton(useButton, controller);
        public override bool used => XCI.GetButtonDown(useButton, controller);
        [SerializeField] XboxButton pickupButton = XboxButton.A;
        public override bool pickingUp => XCI.GetButton(pickupButton, controller);
        public override bool pickedUp => XCI.GetButtonDown(pickupButton, controller);
        [SerializeField] XboxButton dashButton = XboxButton.B;
        public override bool dashing => XCI.GetButton(dashButton, controller);
        public override bool dashed => XCI.GetButtonDown(dashButton, controller);

		[SerializeField] XboxButton panicButton = XboxButton.Y;
		public override bool panicking => XCI.GetButton(panicButton, controller);
		public override bool panicked => XCI.GetButtonDown(panicButton, controller);
	}
}