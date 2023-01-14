using ProjectCanteen.BLL.DTOs.ClassTeacherDTO;

namespace ProjectCanteen.BLL.Services.Interfaces
{
    public interface IClassTeacherService
    {
        Task<(IEnumerable<FullClassTeacherDTO> teachers, int totalCount)> GetTeachersAsync(int page, int pageSize, int schoolId);
        Task<ClassTeacherDTO> GetTeacherByUserId(string id);
        Task UpdateTeacherAsync(UpdateClassTeacherDTO classTeacherDTO);
        Task<bool> DeleteTeacherAsync(int id);
    }
}
