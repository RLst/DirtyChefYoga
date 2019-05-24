using UnityEngine;
namespace DirtyChefYoga
{
    public class Benchtop : Station
    {
        public override bool Interact(Ingredient ingredient)
        {
            OnInteract.Invoke();
            
            //Place on the work surface
            ingredient.transform.position = workSurface.position;

            //Turn off physics
            ingredient.SetPhysicsActive(false);

            return true;
        }
    }
}
