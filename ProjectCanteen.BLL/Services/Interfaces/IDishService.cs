using ProjectCanteen.BLL.DTOs.Dish;

namespace ProjectCanteen.BLL.Services.Interfaces
{
    public interface IDishService
    {
        Task<(IEnumerable<FullDishDTO> dishes, int totalCount)> GetDishesAsync(int page, int pageSize, int workerId);
        Task CreateDishAsync(CreateDishDTO createDishDTO, int workerId);
        Task UpdateDishAsync(UpdateDishDTO dishDTO);
        Task<bool> IsDishExistWithIdAsync(int id);
        Task<bool> DeleteDishAsync(int id);
    }
}
