using UnityEngine;
namespace DirtyChefYoga
{
    public abstract class Station : MonoBehaviour
    {
        public abstract bool Interact(Ingredient ingredient);
    }
}
