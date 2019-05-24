using UnityEngine;
namespace DirtyChefYoga
{
    public class Bin : Station
    {
        [SerializeField] float destroyDelay = 3f;

        public override bool Interact(Ingredient ingredient)
        {
            OnInteract.Invoke();

            //1. Ingredient get's placed on the surface
            ingredient.transform.position = workSurface.position;

            //2. Falls down due to physics
            ingredient.SetPhysicsActive(true);

            //3. Deletes ingredientes after a certain amount of time
            Destroy(ingredient.gameObject, destroyDelay);

            return true;
        }
    }
}
