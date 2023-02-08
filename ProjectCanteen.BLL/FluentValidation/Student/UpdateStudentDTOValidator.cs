using FluentValidation;
using ProjectCanteen.BLL.DTOs.Student;
using ProjectCanteen.DAL;

namespace ProjectCanteen.BLL.FluentValidation.Student
{
    public class UpdateStudentDTOValidator : AbstractValidator<UpdateStudentDTO>
    {
        public UpdateStudentDTOValidator()
        {
            RuleFor(student => student.Id).GreaterThanOrEqualTo(Constants.MinId);
            RuleFor(student => student.FirstName).NotEmpty().Length(Constants.MinNameLength, Constants.MaxNameLength);
            RuleFor(student => student.LastName).NotEmpty().Length(Constants.MinNameLength, Constants.MaxNameLength);
            RuleFor(student => student.Patronymic).Length(Constants.MinNameLength, Constants.MaxNameLength);
            RuleFor(student => student.TagId).Length(Constants.MinTagLength, Constants.MaxTagLength);
            RuleFor(student => student.ClassId).GreaterThanOrEqualTo(Constants.MinId);
            RuleForEach(student => student.ParentsId).GreaterThanOrEqualTo(Constants.MinId);
            RuleForEach(student => student.DietaryRestrictionsId).GreaterThanOrEqualTo(Constants.MinId);
        }
    }
}
