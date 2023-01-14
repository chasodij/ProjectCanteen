using ProjectCanteen.BLL.DTOs.CanteenWorker;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.BLL.Services.Interfaces
{
    public interface ICanteenWorkerService
    {
        Task<(IEnumerable<FullCanteenWorkerDTO> workers, int totalCount)> GetWorkersAsync(int page, int pageSize, int canteenId);
        Task<(bool isExist, int workerId)> GetWorkerIdByUserId(string id);
        Task<CanteenWorker?> GetWorkerByUserId(string id);
        Task UpdateWorkerAsync(UpdateCanteenWorkerDTO canteenWorkerDTO);
        Task<bool> DeleteWorkerAsync(int id);
    }
}
