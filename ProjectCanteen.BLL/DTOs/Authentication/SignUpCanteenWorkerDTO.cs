namespace ProjectCanteen.BLL.DTOs.Authentication
{
    public class SignUpCanteenWorkerDTO : SignUpBaseDTO
    {
        public string LastName { get; set; }
        public string? Patronymic { get; set; }
        public int CanteenId { get; set; }
    }
}
