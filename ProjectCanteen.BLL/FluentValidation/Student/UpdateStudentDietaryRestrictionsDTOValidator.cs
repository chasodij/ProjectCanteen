using FluentValidation;
using ProjectCanteen.BLL.DTOs.Student;
using ProjectCanteen.DAL;

namespace ProjectCanteen.BLL.FluentValidation.Student
{
    public class UpdateStudentDietaryRestrictionsDTOValidator : AbstractValidator<UpdateStudentDietaryRestrictionsDTO>
    {
        public UpdateStudentDietaryRestrictionsDTOValidator()
        {
            RuleFor(x => x.Id).GreaterThanOrEqualTo(Constants.MinId);
            RuleForEach(x => x.DietaryRestrictionsId).GreaterThanOrEqualTo(Constants.MinId);
        }
    }
}
