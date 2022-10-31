namespace ProjectCanteen.DAL.Entities
{
    public class OrderItem
    {
        public Order Order { get; set; }
        public Dish Dish { get; set; }
        public int Portions { get; set; }
        public decimal DishPrice { get; set; }
    }
}
