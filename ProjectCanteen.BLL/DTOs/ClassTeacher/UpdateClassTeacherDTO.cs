namespace ProjectCanteen.BLL.DTOs.ClassTeacherDTO
{
    public class UpdateClassTeacherDTO
    {
        public int Id { get; set; }
        public int ClassId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Patronymic { get; set; }
    }
}
