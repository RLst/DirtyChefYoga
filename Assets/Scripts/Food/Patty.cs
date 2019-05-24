using UnityEngine;
namespace DirtyChefYoga
{
	public class Patty : BurgerIngredient
	{
		Material mat;
		public override float cookProgress
		{
			set
			{
				//Set as usual
				m_cookProgress = value;
				if (m_cookProgress > 2f) m_cookProgress = 2f;

				//Also set the shader's burn value
				mat.SetFloat("_CookAmount", m_cookProgress);
			}
		}
		private void Start()
		{
			mat = GetComponentInChildren<MeshRenderer>().material;
		}
	}

}