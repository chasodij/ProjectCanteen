using ProjectCanteen.BLL.DTOs.Student;

namespace ProjectCanteen.BLL.Services.Interfaces
{
    public interface IStudentService
    {
        Task<(IEnumerable<FullStudentDTO> students, int totalCount)> GetStudentsAsync(int page, int pageSize);
        Task UpdateStudentDietaryRestrictionsAsync(UpdateStudentDietaryRestrictionsDTO restrictionsDTO);
        Task UpdateStudentTagAsync(UpdateStudentTagDTO tagDTO);
        Task UpdateStudentAsync(UpdateStudentDTO studentDTO);
        Task<bool> DeleteStudentAsync(int id);
    }
}
