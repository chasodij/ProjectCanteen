using ProjectCanteen.BLL.DTOs.MenuOfTheDay;

namespace ProjectCanteen.BLL.Services.Interfaces
{
    public interface IMenuOfTheDayService
    {
        Task<IEnumerable<MenuOfTheDayDTO>> GetMenuOfTheDayAsync();
        Task CreateMenuOfTheDayAsync(CreateMenuOfTheDayDTO createMenuOfTheDayDTO);
        Task UpdateMenuOfTheDayAsync(MenuOfTheDayDTO menuOfTheDayDTO);
        Task<bool> IsMenuOfTheDayExistWithIdAsync(int id);
        Task<bool> DeleteMenuOfTheDayAsync(int id);
    }
}
