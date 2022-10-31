namespace ProjectCanteen.DAL.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderTime { get; set; }
        public Parent Purchaser { get; set; }
        public Student Student { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
