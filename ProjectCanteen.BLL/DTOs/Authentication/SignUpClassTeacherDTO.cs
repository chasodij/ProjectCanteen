namespace ProjectCanteen.BLL.DTOs.Authentication
{
    public class SignUpClassTeacherDTO : SignUpBaseDTO
    {
        public string LastName { get; set; }
        public string? Patronymic { get; set; }
        public int ClassId { get; set; }
    }
}
