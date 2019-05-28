using UnityEngine;
namespace DirtyChefYoga
{
	public class Patty : BurgerIngredient
	{
		Material mat;	//Access the shader so we can change it's cooked appearance
		public override float cookAmount
		{
			set
			{
				//Set as usual (this hopefully invokes the base property's setter... IT DOES!)
				base.cookAmount = value;

				//Also set the shader's burn value
				mat.SetFloat("_CookAmount", m_cookAmount);
			}
		}
		private void Start()
		{
			mat = GetComponentInChildren<MeshRenderer>().material;
		}
	}

}