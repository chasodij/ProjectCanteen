using ProjectCanteen.BLL.DTOs.Canteen;

namespace ProjectCanteen.BLL.Services.Interfaces
{
    public interface ICanteenService
    {
        Task<(IEnumerable<FullCanteenDTO> canteens, int totalCount)> GetCanteensAsync(int page, int pageSize, int schoolId);
        Task CreateCanteenAsync(CreateCanteenDTO createCanteenDTO);
        Task UpdateCanteenAsync(UpdateCanteenDTO canteenDTO);
        Task<bool> IsCanteenExistWithIdAsync(int id);
        Task<bool> DeleteCanteenAsync(int id);
    }
}
