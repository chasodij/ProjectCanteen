using FluentValidation;
using ProjectCanteen.BLL.DTOs.Class;
using ProjectCanteen.DAL;

namespace ProjectCanteen.BLL.FluentValidation.Class
{
    public class CreateClassDTOValidator : AbstractValidator<CreateClassDTO>
    {
        public CreateClassDTOValidator()
        {
            RuleFor(cur_class => cur_class.ClassName).NotEmpty().Length(Constants.MinTitleLength, Constants.MaxTitleLength);
            RuleFor(cur_class => cur_class.SchoolId).GreaterThanOrEqualTo(Constants.MinId);
        }
    }
}
