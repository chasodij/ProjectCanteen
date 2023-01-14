namespace ProjectCanteen.BLL.DTOs.Student
{
    public class UpdateStudentDietaryRestrictionsDTO
    {
        public int Id { get; set; }
        public List<int> DietaryRestrictionsId { get; set; }
    }
}
