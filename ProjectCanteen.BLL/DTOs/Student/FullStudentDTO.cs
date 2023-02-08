using ProjectCanteen.BLL.DTOs.DietaryRestriction;
using ProjectCanteen.BLL.DTOs.Parent;

namespace ProjectCanteen.BLL.DTOs.Student
{
    public class FullStudentDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Patronymic { get; set; }
        public string? TagId { get; set; }
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public bool IsAllowedToUseAccount { get; set; }
        public List<FullParentWithoutChildrenDTO> Parents { get; set; }
        public List<DietaryRestrictionDTO> DietaryRestrictions { get; set; }
    }
}
