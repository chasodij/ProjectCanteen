using ProjectCanteen.DAL.Repositories.Interfaces.Base;

namespace ProjectCanteen.DAL.Repositories.Interfaces.ClassTeacher
{
    public interface IClassTeacherRepository : IBaseRepository<Entities.ClassTeacher>
    {
        Task<Entities.ClassTeacher?> GetByIdAsync(int id);
    }
}
