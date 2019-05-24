using UnityEngine.Assertions;
using UnityEngine;
namespace DirtyChefYoga
{
    //Handles player interactions
    public class PlayerInteract : MonoBehaviour
    {
        [SerializeField] Transform hand;
        [SerializeField] Vector3 castExtents = new Vector3(0.3f, 1, 0.5f);
        [SerializeField] float castLength;

        Ingredient itemHeld;

        void Start()
        {
            Assert.IsNotNull(hand, "No hand transform found!");
        }

        public void PickUpIngredient(Ingredient Ingredient)
        {

        }

        //Detect object of type T according to set cast paramters
        public bool DetectInteractable<T>(out T interactableFound) where T : MonoBehaviour
        {
            bool isHit = Physics.BoxCast(transform.position, castExtents, transform.forward, out RaycastHit hit, transform.rotation, castLength);
            T hitComponent = hit.collider.GetComponent<T>();

            //If something hit
            if (isHit)
            {
                //Get found component
                if (hitComponent is T) //item is the right type!
                {
                    //set found object
                    interactableFound = hitComponent;
                    return true;
                }
            }
            //Nothing found
            interactableFound = null;
            return false;
        }
    }
}