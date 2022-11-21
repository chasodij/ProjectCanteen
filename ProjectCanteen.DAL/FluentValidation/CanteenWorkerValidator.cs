using FluentValidation;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.DAL.FluentValidation
{
    public class CanteenWorkerValidator : AbstractValidator<CanteenWorker>
    {
        public CanteenWorkerValidator()
        {
            RuleFor(worker => worker.User).NotNull().SetValidator(new UserValidator());
            RuleFor(worker => worker.Canteen).NotNull().SetValidator(new CanteenValidator());
        }
    }
}
