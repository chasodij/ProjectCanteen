using ProjectCanteen.BLL.DTOs.CanteenWorker;
using ProjectCanteen.BLL.DTOs.Class;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.BLL.Services.Interfaces
{
    public interface IClassService
    {
        Task<(IEnumerable<FullClassDTO> classes, int totalCount)> GetClassesAsync(int page, int pageSize, int schoolId);
        Task CreateClassAsync(CreateClassDTO createClassDTO);
        Task UpdateClassAsync(UpdateClassDTO classDTO);
        Task<Class> GetClassByTeacherId(int id);
        Task<bool> DeleteClassAsync(int id);
        Task<ClassOrdersDTO> GetClassOrders(int classId, DateTime firstDate, DateTime lastDate);
    }
}
