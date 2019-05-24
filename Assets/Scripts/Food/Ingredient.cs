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

    public abstract class Ingredient : MonoBehaviour
    {
        // [SerializeField] GameObject uncookedPrefab; //If this ingredient can't be cooked then this is the default
        [SerializeField] bool m_isCookable = true;
        public bool isCookable { 
            get { return m_isCookable; }
            private set { m_isCookable = value; } 
        }
        public float cookProgress { get; set; } = 0f;
        public CookStatus cookStatus    
        {
            get
            {
                if (isCookable)
                    return (CookStatus)Convert.ToInt32(cookProgress);
                else
                    return CookStatus.NA;
                    // //Item cannot be cooked; Invalid call
                    // throw new InvalidOperationException();
            }
        }

        
        Rigidbody rb;
        Collider col;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            col = GetComponent<Collider>();
        }

        public void SetPhysicsActive(bool active)
        {
            if (active)
            {
                rb.isKinematic = false;
                col.isTrigger = false;
            }
            else
            {
                rb.isKinematic = true;  //is not affected by physics ie. gravity
                col.isTrigger = true;  //Can't be affect by other physics objects
            }
        }        
    }
}