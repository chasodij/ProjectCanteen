using ProjectCanteen.BLL.DTOs.Student;

namespace ProjectCanteen.BLL.DTOs.Parent
{
    public class FullParentDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Patronymic { get; set; }
        public List<FullStudentWithoutParentsDTO> Children { get; set; }
    }
}
