using ProjectCanteen.DAL.Repositories.Interfaces.Base;

namespace ProjectCanteen.DAL.Repositories.Interfaces.Student
{
    public interface IStudentRepository : IBaseRepository<Entities.Student>
    {
        Task<Entities.Student?> GetByIdAsync(int id);
        Task UpdateRestrictionsAsync(Entities.Student student);
        Task UpdateTagAsync(Entities.Student student);
    }
}
