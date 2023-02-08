using ProjectCanteen.BLL.DTOs.Base;
using ProjectCanteen.BLL.DTOs.Dish;
using ProjectCanteen.BLL.DTOs.MenuOfTheDay;

namespace ProjectCanteen.BLL.Services.Interfaces
{
    public interface IMenuOfTheDayService
    {
        Task<(IEnumerable<FullMenuOfTheDayDTO> menus, int totalCount)> GetMenusAsync(int page, int pageSize, int workerId);
        Task<IEnumerable<FullDishDTO>> GetMenuDishesForStudent(int studentId, DateTime date, int canteenId);
        Task<BaseResponseDTO> CreateMenuAsync(CreateMenuOfTheDayDTO createMenuOfTheDayDTO, int workerId);
        Task UpdateMenuAsync(UpdateMenuOfTheDayDTO menuOfTheDayDTO);
        Task<bool> IsMenuExistWithIdAsync(int id);
        Task<bool> DeleteMenuAsync(int id);
    }
}
