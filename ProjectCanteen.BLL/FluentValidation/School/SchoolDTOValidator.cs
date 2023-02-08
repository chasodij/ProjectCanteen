using FluentValidation;
using ProjectCanteen.BLL.DTOs.School;
using ProjectCanteen.DAL;

namespace ProjectCanteen.BLL.FluentValidation.School
{
    public class SchoolDTOValidator : AbstractValidator<SchoolDTO>
    {
        public SchoolDTOValidator()
        {
            RuleFor(school => school.Id).GreaterThanOrEqualTo(Constants.MinId);
            RuleFor(school => school.Name).NotEmpty().Length(Constants.MinTitleLength, Constants.MaxTitleLength);
        }
    }
}
