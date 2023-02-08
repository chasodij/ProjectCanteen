namespace ProjectCanteen.BLL.DTOs.Student
{
    public class UpdateStudentDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Patronymic { get; set; }
        public string? TagId { get; set; }
        public int ClassId { get; set; }
        public bool IsAllowedToUseAccount { get; set; }
        public List<int> ParentsId { get; set; }
        public List<int> DietaryRestrictionsId { get; set; }
    }
}
