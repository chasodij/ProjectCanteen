using FluentValidation;
using ProjectCanteen.BLL.DTOs.MenuOfTheDay;
using ProjectCanteen.DAL;

namespace ProjectCanteen.BLL.FluentValidation.MenuOfTheDay
{
    public class MenuOfTheDayDTOValidator : AbstractValidator<UpdateMenuOfTheDayDTO>
    {
        public MenuOfTheDayDTOValidator()
        {
            RuleFor(menu => menu.Id).GreaterThanOrEqualTo(Constants.MinId);
            RuleFor(menu => menu.Day).NotNull().GreaterThan(Constants.MinDate);
            RuleForEach(menu => menu.DishesId).GreaterThanOrEqualTo(Constants.MinId);
        }
    }
}
