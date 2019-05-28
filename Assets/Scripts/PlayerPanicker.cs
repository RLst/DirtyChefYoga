
using UnityEngine;
namespace DirtyChefYoga
{
	//Handles the characters panicking animation
	//This is more for show
	public class PlayerPanicker : MonoBehaviour
	{
		PlayerActions actioner;
		PlayerInput input;
		Animator anim;
		void Awake()
		{
			anim = GetComponentInChildren<Animator>();
			input = GetComponent<PlayerInput>();
			actioner = GetComponent<PlayerActions>();
		}

		void Update()
		{
			ControlPanicking();
		}

		private void ControlPanicking()
		{
			if (input.panicking)
			{
				actioner.DropItem();
				anim.SetBool("isPanic", true);
			}
			else
			{
				anim.SetBool("isPanic", false);
			}
		}
	}
}