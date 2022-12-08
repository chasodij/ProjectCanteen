using ProjectCanteen.BLL.DTOs.Dish;

namespace ProjectCanteen.BLL.Services.Interfaces
{
    public interface IDishService
    {
        Task<IEnumerable<DishDTO>> GetDishesAsync();
        Task CreateDishAsync(CreateDishDTO createDishDTO);
        Task UpdateDishAsync(DishDTO dishDTO);
        Task<bool> IsDishExistWithIdAsync(int id);
        Task<bool> DeleteDishAsync(int id);
    }
}
