using FluentValidation;
using ProjectCanteen.BLL.DTOs.CanteenWorker;
using ProjectCanteen.DAL;

namespace ProjectCanteen.BLL.FluentValidation.CanteenWorker
{
    public class UpdateCanteenWorkerDTOValidator : AbstractValidator<UpdateCanteenWorkerDTO>
    {
        public UpdateCanteenWorkerDTOValidator()
        {
            RuleFor(worker => worker.Id).GreaterThanOrEqualTo(Constants.MinId);
            RuleFor(worker => worker.CanteenId).GreaterThanOrEqualTo(Constants.MinId);
            RuleFor(worker => worker.FirstName).NotEmpty().Length(Constants.MinNameLength, Constants.MaxNameLength);
            RuleFor(worker => worker.LastName).NotEmpty().Length(Constants.MinNameLength, Constants.MaxNameLength);
            RuleFor(worker => worker.Patronymic).Length(Constants.MinNameLength, Constants.MaxNameLength);
        }
    }
}
