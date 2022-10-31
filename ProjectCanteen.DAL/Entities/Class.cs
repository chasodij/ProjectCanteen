namespace ProjectCanteen.DAL.Entities
{
    public class Class
    {
        public int Id { get; set; }
        public string ClassName { get; set; } = String.Empty;
        public ClassTeacher ClassTeacher { get; set; }
        public School School { get; set; }
        public List<Student> Students { get; set; } = new List<Student>();
    }
}
