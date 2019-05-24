using System.Collections;
using UnityEngine;
namespace DirtyChefYoga.Util
{
	//Just provides an easy way to create a visual alert
	public class ParticleAlert : MonoBehaviour
	{
		[SerializeField] ParticleSystem particle;
		
		public void Activate(float duration)
		{
			particle.Play();
			StartCoroutine(timer(duration));

			IEnumerator timer(float delay)
			{
				while (true)
				{
					yield return new WaitForSeconds(delay);
					particle.Pause();
					break;
				}
			}
		}
	}
}