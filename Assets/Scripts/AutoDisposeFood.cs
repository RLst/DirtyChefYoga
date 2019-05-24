using UnityEngine;

namespace DirtyChefYoga
{
    [RequireComponent(typeof(Collider))]
    public class AutoDisposeFood : MonoBehaviour
    {
        Collider col;

        void Start()
        {
            col = GetComponent<Collider>();
        }
        void OnCollisionEnter(Collision col)
        {
            var hit = col.collider.GetComponent<Ingredient>();
            if (hit != null)
            {
                Destroy(hit.gameObject);
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, new Vector3(0.3f, 3, 0.3f));
        }
    }
}
