using Microsoft.AspNetCore.Identity;

namespace ProjectCanteen.DAL.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Patronymic { get; set; }
    }
}
