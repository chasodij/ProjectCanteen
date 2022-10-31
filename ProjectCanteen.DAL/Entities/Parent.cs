namespace ProjectCanteen.DAL.Entities
{
    public class Parent
    {
        public int Id { get; set; }
        public List<Student> Children { get; set; } = new List<Student>();
        public User User { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
