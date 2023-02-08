namespace ProjectCanteen.BLL.DTOs.ClassTeacherDTO
{
    public class FullClassTeacherDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Patronymic { get; set; }
    }
}
