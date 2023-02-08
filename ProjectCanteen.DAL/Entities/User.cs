using Microsoft.AspNetCore.Identity;

namespace ProjectCanteen.DAL.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Patronymic { get; set; }
        public SchoolAdmin? SchoolAdmin { get; set; }
        public ClassTeacher? ClassTeacher { get; set; }
        public CanteenWorker? CanteenWorker { get; set; }
        public Parent? Parent { get; set; }
        public Student? Student { get; set; }
    }
}
