using FluentValidation;
using ProjectCanteen.BLL.DTOs.Student;
using ProjectCanteen.DAL;

namespace ProjectCanteen.BLL.FluentValidation.Student
{
    public class UpdateStudentTagDTOValidator : AbstractValidator<UpdateStudentTagDTO>
    {
        public UpdateStudentTagDTOValidator()
        {
            RuleFor(x => x.Id).GreaterThanOrEqualTo(Constants.MinId);
            RuleFor(x => x.TagId).Length(Constants.MinTagLength, Constants.MaxTagLength);
        }
    }
}
