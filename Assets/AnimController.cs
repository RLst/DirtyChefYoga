using UnityEngine;
namespace DirtyChefYoga
{
	public class AnimController : MonoBehaviour
	{

		[Header("Animation Names")]
		[SerializeField] string idle = "Idle";
		[SerializeField] string holding = "Holding";
		[SerializeField] string panic = "Panic";
		
		Animator anim;

		void Awake()
		{
			anim = GetComponentInChildren<Animator>();
		}

		public void OnIdle()
		{
			anim.SetTrigger(idle);
		}
		public void OnHold()
		{
			anim.SetTrigger(holding);
		}
	}
}
