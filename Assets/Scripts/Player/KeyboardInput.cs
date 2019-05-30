using UnityEngine;
namespace DirtyChefYoga
{
	public class KeyboardInput : PlayerInput
	{
		[Header("Left Axis")]
		[SerializeField] string leftAxisXName = "Horizontal";
		[SerializeField] string leftAxisYName = "Vertical";
		public override Vector2 leftAxis
		{
			get
			{
				Vector2 result;
				//Raw if needed
				if (useRaw)
				{
					result.x = Input.GetAxisRaw(leftAxisXName);
					result.y = Input.GetAxisRaw(leftAxisYName);
				}
				else
				{
					result.x = Input.GetAxis(leftAxisXName);
					result.y = Input.GetAxis(leftAxisYName);
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
		[SerializeField] string rightAxisXName = "Horizontal";
		[SerializeField] string rightAxisYName = "Vertical";
		public override Vector2 rightAxis
		{
			get
			{
				Vector2 result;
				//Raw if needed
				if (useRaw)
				{
					result.x = Input.GetAxisRaw(rightAxisXName);
					result.y = Input.GetAxisRaw(rightAxisYName);
				}
				else
				{
					result.x = Input.GetAxis(rightAxisXName);
					result.y = Input.GetAxis(rightAxisYName);
				}

				//Inverse if needed
				if (invertXaxis)
					result.x = -result.x;

				if (invertYaxis)
					result.y = -result.y;

				return result;
			}
		}

		[Header("Extra Axis")]
		[SerializeField] string moveAxis = "Horizontal";
		public override float move => Input.GetAxis(moveAxis);


		[Header("Actions")]
		[SerializeField] KeyCode useKey = KeyCode.J;
		public override bool @using => Input.GetKey(useKey);
		public override bool used => Input.GetKeyDown(useKey);

		[SerializeField] KeyCode pickUpKey = KeyCode.K;
		public override bool pickingUp => Input.GetKey(pickUpKey);
		public override bool pickedUp => Input.GetKeyDown(pickUpKey);

		[SerializeField] KeyCode dashKey = KeyCode.L;
		public override bool dashing => Input.GetKey(dashKey);
		public override bool dashed => Input.GetKeyDown(dashKey);

		[SerializeField] KeyCode panicKey = KeyCode.P;
		public override bool panicking => Input.GetKey(panicKey);
		public override bool panicked => Input.GetKeyDown(panicKey);
	}
}