using FluentValidation;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.DAL.FluentValidation
{
    public class SchoolAdminValidator : AbstractValidator<SchoolAdmin>
    {
        public SchoolAdminValidator()
        {
            RuleFor(admin => admin.User).SetValidator(new UserValidator());
            RuleFor(admin => admin.School).SetValidator(new SchoolValidator());
        }
    }
}
