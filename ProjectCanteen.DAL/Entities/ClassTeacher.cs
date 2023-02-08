namespace ProjectCanteen.DAL.Entities
{
    public class ClassTeacher
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int ClassId { get; set; }
        public Class Class { get; set; }
    }
}
