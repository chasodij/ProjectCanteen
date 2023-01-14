namespace ProjectCanteen.BLL.DTOs.Authentication
{
    public class SignUpSchoolAdminDTO : SignUpBaseDTO
    {
        public string LastName { get; set; }
        public string? Patronymic { get; set; }
        public int SchoolId { get; set; }
    }
}
