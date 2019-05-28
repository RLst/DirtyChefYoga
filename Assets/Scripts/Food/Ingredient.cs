using System;
using UnityEngine;

namespace DirtyChefYoga
{
	public enum CookStatus
	{
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
		[Tooltip("How many score points this ingredient is worth")]
		[SerializeField] int m_scoreValue = 1;
		public int scoreValue { get { return m_scoreValue; } }

		#region COOKABILITY
		public bool isCookable
		{
			get { return m_isCookable; }
			private set { m_isCookable = value; }
		}
		[SerializeField] bool m_isCookable = true;
		protected float m_cookAmount = 0f;
		public virtual float cookAmount
		{
			get { return m_cookAmount; }
			set
			{
				m_cookAmount = value;
			}
		}
		public CookStatus cookStatus
		{
			get
			{
				if (isCookable)
				{
					//Round to the lowest integer, clamp between Uncooked and Overcooked
					//cookamount = 0.2 => 0 => CookStatus.Uncooked
					//cookamount = 0.9 => 0 => CookStatus.Uncooked
					//cookamount = 1.0 => 1 => CookStatus.Cooked
					//cookamount = 1.2 => 1 => CookStatus.Cooked
					//cookamount = 1.9 => 1 => CookStatus.Cooked
					//cookamount = 2.0 => 2 => CookStatus.Overcooked
					//cookamount = 2.5 => 2 => CookStatus.Overcooked
					//cookamount = 10.0 => 2 => CookStatus.Overcooked
					int cookedValue = Mathf.FloorToInt(cookAmount);
					cookedValue = Mathf.Clamp(cookedValue, (int)CookStatus.UnCooked, (int)CookStatus.OverCooked);
					return (CookStatus)cookedValue;
				}
				else
					return CookStatus.NA;
			}
		}
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
			if (active)
			{
				rb.constraints = RigidbodyConstraints.None;
				col.isTrigger = false;
			}
			else
			{
				rb.constraints = RigidbodyConstraints.FreezeAll;
				col.isTrigger = true;
			}
		}
	}
}
