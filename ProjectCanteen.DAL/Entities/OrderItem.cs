namespace ProjectCanteen.DAL.Entities
{
    public class OrderItem
    {
        public int OrderId { get; set; }
        public int DishId { get; set; }
        public Order Order { get; set; }
        public Dish Dish { get; set; }
        public int Portions { get; set; }
        public decimal DishPrice { get; set; }
    }
}
