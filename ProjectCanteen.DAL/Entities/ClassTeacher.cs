namespace ProjectCanteen.DAL.Entities
{
    public class ClassTeacher
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Class Class { get; set; }
    }
}
