using FluentValidation;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.DAL.FluentValidation
{
    public class MenuOfTheDayValidator : AbstractValidator<MenuOfTheDay>
    {
        public MenuOfTheDayValidator()
        {
            RuleFor(menu => menu.Canteen).NotNull().SetValidator(new CanteenValidator());
            RuleFor(menu => menu.Day).NotNull().GreaterThan(Constants.MinDate);
            RuleForEach(menu => menu.Dishes).SetValidator(new DishValidator());
        }
    }
}
