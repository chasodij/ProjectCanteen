namespace ProjectCanteen.DAL.Entities
{
    public class Dish
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public decimal Price { get; set; }
        public List<IngredientInDish> IngredientInDishes { get; set; } = new List<IngredientInDish>();
        public Canteen Canteen { get; set; }
        public List<MenuOfTheDay> MenuOfTheDays { get; set; } = new List<MenuOfTheDay>();
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
