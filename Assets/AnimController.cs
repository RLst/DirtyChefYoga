using UnityEngine;
namespace DirtyChefYoga
{
	public class AnimController : MonoBehaviour
	{

		[SerializeField] string idleAnimTag = "Idle";
		[SerializeField] string holdAnimTag = "Holding";
		// [SerializeField] string pickupAnimTag = "PickingUp";
		Animator anim;

		void Awake()
		{
			anim = GetComponentInChildren<Animator>();
		}

		public void OnIdle()
		{
			anim.SetTrigger(idleAnimTag);
		}
		public void OnHold()
		{
			anim.SetTrigger(holdAnimTag);
		}
	}
}
