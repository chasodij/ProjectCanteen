using FluentValidation;
using ProjectCanteen.BLL.DTOs.ClassTeacherDTO;
using ProjectCanteen.DAL;

namespace ProjectCanteen.BLL.FluentValidation.ClassTeacher
{
    public class UpdateClassTeacherDTOValidator : AbstractValidator<UpdateClassTeacherDTO>
    {
        public UpdateClassTeacherDTOValidator()
        {
            RuleFor(teacher => teacher.Id).GreaterThanOrEqualTo(Constants.MinId);
            RuleFor(teacher => teacher.ClassId).GreaterThanOrEqualTo(Constants.MinId);
            RuleFor(teacher => teacher.FirstName).NotEmpty().Length(Constants.MinNameLength, Constants.MaxNameLength);
            RuleFor(teacher => teacher.LastName).NotEmpty().Length(Constants.MinNameLength, Constants.MaxNameLength);
            RuleFor(teacher => teacher.Patronymic).Length(Constants.MinNameLength, Constants.MaxNameLength);
        }
    }
}
