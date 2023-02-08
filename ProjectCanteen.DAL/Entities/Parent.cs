namespace ProjectCanteen.DAL.Entities
{
    public class Parent
    {
        public int Id { get; set; }
        public List<Student> Children { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public List<Order> Orders { get; set; }
    }
}
