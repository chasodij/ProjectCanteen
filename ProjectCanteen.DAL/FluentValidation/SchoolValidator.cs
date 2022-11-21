using FluentValidation;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.DAL.FluentValidation
{
    public class SchoolValidator : AbstractValidator<School>
    {
        public SchoolValidator()
        {
            RuleFor(school => school.Name).NotEmpty().Length(Constants.MinTitleLength, Constants.MaxTitleLength);
            RuleForEach(school => school.Classes).SetValidator(new ClassValidator());
            RuleForEach(school => school.Canteens).SetValidator(new CanteenValidator());
        }
    }
}
