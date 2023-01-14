using FluentValidation;
using ProjectCanteen.BLL.DTOs.School;
using ProjectCanteen.DAL;

namespace ProjectCanteen.BLL.FluentValidation.School
{
    public class CreateSchoolDTOValidator : AbstractValidator<CreateSchoolDTO>
    {
        public CreateSchoolDTOValidator()
        {
            RuleFor(school => school.Name).NotEmpty().Length(Constants.MinTitleLength, Constants.MaxTitleLength);
        }
    }
}
