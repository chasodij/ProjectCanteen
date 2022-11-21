using FluentValidation;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.DAL.FluentValidation
{
    public class CanteenValidator : AbstractValidator<Canteen>
    {
        public CanteenValidator()
        {
            RuleFor(canteen => canteen.Name).NotNull().NotEmpty().MaximumLength(Constants.MaxTitleLength);
            RuleFor(canteen => canteen.School).NotNull().SetValidator(new SchoolValidator());
            RuleFor(canteen => canteen.Terminal).SetValidator(new UserValidator());
            RuleForEach(canteen => canteen.CanteenWorkers).SetValidator(new CanteenWorkerValidator());
            RuleForEach(canteen => canteen.Dishes).SetValidator(new DishValidator());
            RuleForEach(canteen => canteen.Ingredients).SetValidator(new IngredientValidator());
            RuleForEach(canteen => canteen.MenuOfTheDays).SetValidator(new MenuOfTheDayValidator());
        }
    }
}
