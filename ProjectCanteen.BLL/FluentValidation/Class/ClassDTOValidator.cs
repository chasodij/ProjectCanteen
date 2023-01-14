using FluentValidation;
using ProjectCanteen.BLL.DTOs.Class;
using ProjectCanteen.DAL;

namespace ProjectCanteen.BLL.FluentValidation.Class
{
    public class ClassDTOValidator : AbstractValidator<UpdateClassDTO>
    {
        public ClassDTOValidator()
        {
            RuleFor(cur_class => cur_class.Id).GreaterThanOrEqualTo(Constants.MinId);
            RuleFor(cur_class => cur_class.ClassName).NotEmpty().Length(Constants.MinTitleLength, Constants.MaxTitleLength);
            RuleFor(cur_class => cur_class.SchoolId).GreaterThanOrEqualTo(Constants.MinId);
        }
    }
}
