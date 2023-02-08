using FluentValidation;
using ProjectCanteen.BLL.DTOs.MenuOfTheDay;
using ProjectCanteen.DAL;

namespace ProjectCanteen.BLL.FluentValidation.MenuOfTheDay
{
    public class CreateMenuOfTheDayDTOValidator : AbstractValidator<CreateMenuOfTheDayDTO>
    {
        public CreateMenuOfTheDayDTOValidator()
        {
            RuleFor(menu => menu.Day).NotNull().GreaterThan(Constants.MinDate);
            RuleForEach(menu => menu.DishesId).GreaterThanOrEqualTo(Constants.MinId);
        }
    }
}
