using ProjectCanteen.BLL.DTOs.School;

namespace ProjectCanteen.BLL.Services.Interfaces
{
    public interface ISchoolService
    {
        Task<IEnumerable<SchoolDTO>> GetSchoolsAsync();
        Task CreateSchoolAsync(CreateSchoolDTO createSchoolDTO);
        Task UpdateSchoolAsync(SchoolDTO choolDTO);
        Task<bool> IsSchoolExistWithIdAsync(int id);
        Task<bool> DeleteSchoolAsync(int id);
    }
}
