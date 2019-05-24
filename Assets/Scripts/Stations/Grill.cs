namespace DirtyChefYoga
{
    public class Grill : CookingStation
    {
        public override bool Interact(Ingredient ingredient)
        {
            if (ingredient is Patty)
            {
                return base.Interact(ingredient);
            }
            return false;
        }
    }
}
