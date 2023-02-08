using ProjectCanteen.BLL.DTOs.Parent;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.BLL.Services.Interfaces
{
    public interface IParentService
    {
        Task<(IEnumerable<FullParentDTO> parents, int totalCount)> GetParentsAsync(int page, int pageSize);
        Task UpdateParentAsync(UpdateParentDTO parentDTO);
        Task<Parent?> GetParentByUserId(string id);
        Task<bool> DeleteParentAsync(int id);
    }
}
