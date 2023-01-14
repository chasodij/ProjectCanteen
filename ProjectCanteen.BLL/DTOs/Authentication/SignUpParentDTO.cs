namespace ProjectCanteen.BLL.DTOs.Authentication
{
    public class SignUpParentDTO : SignUpBaseDTO
    {
        public string LastName { get; set; }
        public string? Patronymic { get; set; }
        public List<int> ChildrenId { get; set; }
    }
}
