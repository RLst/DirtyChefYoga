using System;
using UnityEngine;

namespace DirtyChefYoga
{
	public enum CookStatus {
		NA = -1,
		UnCooked = 0,
		Cooked = 1,
		OverCooked = 2
	}

	[RequireComponent(typeof(BoxCollider))]
	[RequireComponent(typeof(Rigidbody))]
	[SelectionBase]
	public abstract class Ingredient : MonoBehaviour
	{
	#region COOKABLE
		public bool isCookable {
			get { return m_isCookable; }
			private set { m_isCookable = value; } }
		[SerializeField] bool m_isCookable = true;
	#endregion

	#region COOKPROGRESS
		public virtual float cookProgress {
			get { return m_cookProgress; }
			set	{
				m_cookProgress = value;
				if (m_cookProgress > 2f) m_cookProgress = 2f; 
			} }   //Limit to 2 max otherwise the shader doesn't like it
		public CookStatus cookStatus {
			get {
				if (isCookable)
					return (CookStatus)Convert.ToInt32(cookProgress);
				else
					return CookStatus.NA;
			} }
		protected float m_cookProgress = 0f;
	#endregion

		protected Rigidbody rb;
		protected Collider col;

		void Awake()
		{
			rb = GetComponent<Rigidbody>();
			col = GetComponent<Collider>();
		}

		public void SetPhysicsActive(bool active)
		{
			if (active) {
				rb.constraints = RigidbodyConstraints.None;
				col.isTrigger = false;
			}
			else {
				rb.constraints = RigidbodyConstraints.FreezeAll;
				col.isTrigger = true;  //Can't be affect by other physics objects
			}
		}
	}
}
