using UnityEngine;

namespace DirtyChefYoga
{
    public class AutoDisposeFood : MonoBehaviour
    {
        void OnCollisionEnter(Collision col)
        {
            var hit = col.collider.GetComponent<Ingredient>();
            if (hit != null)
            {
                Destroy(hit.gameObject);
            }
        }
    }
}
