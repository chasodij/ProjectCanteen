using Microsoft.EntityFrameworkCore;
using ProjectCanteen.DAL.Repositories.Implementations.NewFolder;
using ProjectCanteen.DAL.Repositories.Interfaces.Class;

namespace ProjectCanteen.DAL.Repositories.Implementations.Class
{
    public class ClassRepository : BaseRepository<Entities.Class>, IClassRepository
    {
        public ClassRepository()
        {
            DefaultInclude = func => func.Include(x => x.School).Include(x => x.ClassTeacher);
        }
    }
}
