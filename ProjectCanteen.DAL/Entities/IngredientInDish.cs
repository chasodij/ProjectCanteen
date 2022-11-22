namespace ProjectCanteen.DAL.Entities
{
    public class IngredientInDish
    {
        public int IngredientId { get; set; }
        public int DishId { get; set; }
        public Ingredient Ingredient { get; set; }
        public Dish Dish { get; set; }
        public double AmountInGrams { get; set; }
    }
}
