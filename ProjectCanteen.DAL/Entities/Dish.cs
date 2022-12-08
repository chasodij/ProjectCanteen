namespace ProjectCanteen.DAL.Entities
{
    public class Dish
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public MenuSection MenuSection { get; set; }
        public List<IngredientInDish> IngredientInDishes { get; set; }
        public List<MenuOfTheDay> MenuOfTheDays { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
