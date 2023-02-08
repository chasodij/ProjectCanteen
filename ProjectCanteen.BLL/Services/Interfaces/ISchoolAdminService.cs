using ProjectCanteen.BLL.DTOs.Parent;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.BLL.Services.Interfaces
{
    public interface ISchoolAdminService
    {
        Task<(IEnumerable<FullParentDTO> admins, int totalCount)> GetSchoolAdminsAsync(int page, int pageSize);
        Task UpdateSchoolAdminAsync(UpdateParentDTO parentDTO);
        Task<SchoolAdmin?> GetSchoolAdminByUserId(string id);
        Task<bool> DeleteSchoolAdminAsync(int id);
    }
}
