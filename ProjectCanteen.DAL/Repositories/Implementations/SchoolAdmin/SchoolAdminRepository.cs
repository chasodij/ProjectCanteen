using Microsoft.EntityFrameworkCore;
using ProjectCanteen.DAL.Repositories.Implementations.NewFolder;
using ProjectCanteen.DAL.Repositories.Interfaces.SchoolAdmin;

namespace ProjectCanteen.DAL.Repositories.Implementations.SchoolAdmin
{
    public class SchoolAdminRepository : BaseRepository<Entities.SchoolAdmin>, ISchoolAdminRepository
    {
        public SchoolAdminRepository()
        {
            DefaultInclude = func => func.Include(x => x.School)
                                            .ThenInclude(x => x.Canteens)
                                            .ThenInclude(x => x.CanteenWorkers)
                                          .Include(x => x.School)
                                            .ThenInclude(x => x.Classes)
                                            .ThenInclude(x => x.ClassTeacher)
                                          .Include(x => x.User);
        }
    }
}
