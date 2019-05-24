using UnityEngine;
using UnityEngine.Events;

namespace DirtyChefYoga
{
    public abstract class Station : MonoBehaviour
    {
        [SerializeField] protected Transform workSurface;

        public UnityEvent OnInteract;

        //It is assumed the ingredient passed in will be NOT be null
        public abstract bool Interact(Ingredient ingredient);

        void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(workSurface.position, 0.2f);
        }
    }
}
