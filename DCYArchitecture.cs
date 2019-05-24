namespace Brainstorm
{


    public enum CookStatus
    {
        Uncooked = 0,
        Cooked = 1,
        Overcooked = 2
    }


    public abstract class Station : MonoBehaviour, IInteractable
    {   //used to detect that this is a interactable station

        public abstract bool Interact(Ingredient ingredient);
    }

    public abstract class CookingStation : MonoBehaviour, ICanCook
    {
        public event Action OnStartCooking = delegate { };
        public event Action OnCooked = delegate { };
        public event Action OnOvercooked = delegate { };
        public event Action OnBurning = delegate { };


        [SerializeField] protected float cookTime = 5f;       //How long it takes for an ingredient to cook
        [SerializeField] protected List<Ingredient> blacklist;      //List of ingredients this station can't take


        int HP = 100;   //maybe - if the cook station burns and is rendered unusable
        Ingredient cookingIngredient;


        void Update()
        {
            CookCurrentFood();
        }

        //Returns false if there's already food in the cooker
        public override bool Interact(Ingredient ingredient)    //DONE
        {
            //Make sure the food can be cooked
            if (!ingredient.isCookable)
                return false;

            //Start/resume cooking the item
            currentIngredient = newFood;
            OnStartCooking.Invoke();
        }
        public virtual Food RemoveIngredient();   //If player's box cast hits a cookingstation && pickup key hit then removefood
        protected virtual void CookCurrentIngredient()
        {
            //Don't cook if there's nothing to cook
            if (!currentIngredient) return;

            //Will calculate the correct amount to cook the ingredient within set cooktime
            var cookAmount = cookTime * Time.deltaTime;
            currentIngredient.cookProgress += cookAmount;
        }

        protected virtual void HandleEvents()
        {
            //Cooked
            if (currentIngredient.cookProgress > CookStatus.Cooked)
            {
                OnCooked.Invoke();
            }
            //Overcooked
            else if (currentIngredient.cookProgress > CookStatus.Overcooked)
            {
                OnOvercooked.Invoke();
            }

        }
    }
    public class Fryer : CookingStation
    {

        public override bool Cook(Food newFood)
        {

        }
    }
    public class Grill : CookingStation { }
    public class Toaster : CookingStation { }
    public class Bin : Station { }
    public class Pass : Station { }

    public class Order
    {   //An order is a collection of food items
        List<Food> foodItems;
    }
    public abstract class Food
    {   //A food item is a collection of ingredients
        List<Ingredient> ingredient;
    }

    public abstract class Ingredient
    {   //An ingredient is the most basic food unit that can be stacked or combined 
        public bool isCookable = false;
        public float cookProgress = 0f;     //0 uncooked, 1 cooked, 2 overcooked
        public CookStatus cookStatus
        {
            get
            {

                if (isCookable)
                    return (CookStatus)Convert.ToInt32(cookProgress);
                else
                    return new InvalideOperationException();
            }
        }
    }
    public abstract class BurgerIngredient : Ingredient
    {   //Patties, Tomatoes, lettuce, Cheese, etc
        [serailizefield] float m_thickness = 0.1f;
        public float thickness { get; } = 0.1f;
    }

    //-----------------------------------------

}