using FluentValidation;
using ProjectCanteen.BLL.DTOs.Parent;
using ProjectCanteen.DAL;

namespace ProjectCanteen.BLL.FluentValidation.Parent
{
    public class UpdateParentDTOValidator : AbstractValidator<UpdateParentDTO>
    {
        public UpdateParentDTOValidator()
        {
            RuleFor(parent => parent.Id).GreaterThanOrEqualTo(Constants.MinId);
            RuleFor(parent => parent.FirstName).NotEmpty().Length(Constants.MinNameLength, Constants.MaxNameLength);
            RuleFor(parent => parent.LastName).NotEmpty().Length(Constants.MinNameLength, Constants.MaxNameLength);
            RuleFor(parent => parent.Patronymic).Length(Constants.MinNameLength, Constants.MaxNameLength);
            RuleForEach(parent => parent.ChildrenId).GreaterThanOrEqualTo(Constants.MinId);
        }
    }
}
