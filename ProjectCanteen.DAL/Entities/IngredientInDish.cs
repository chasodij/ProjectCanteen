namespace ProjectCanteen.DAL.Entities
{
    public class IngredientInDish
    {
        public Ingredient Ingredient { get; set; }
        public Dish Dish { get; set; }
        public double AmountInGrams { get; set; }
    }
}
