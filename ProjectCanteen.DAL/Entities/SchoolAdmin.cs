namespace ProjectCanteen.DAL.Entities
{
    public class SchoolAdmin
    {
        public int Id { get; set; }
        public User User { get; set; }
        public School School { get; set; }
    }
}
