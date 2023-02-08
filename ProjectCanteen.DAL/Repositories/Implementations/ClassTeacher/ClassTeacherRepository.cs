using Microsoft.EntityFrameworkCore;
using ProjectCanteen.DAL.Entities;
using ProjectCanteen.DAL.Repositories.Implementations.NewFolder;
using ProjectCanteen.DAL.Repositories.Interfaces.ClassTeacher;
using System.Security.Cryptography.X509Certificates;

namespace ProjectCanteen.DAL.Repositories.Implementations.ClassTeacher
{
    public class ClassTeacherRepository : BaseRepository<Entities.ClassTeacher>, IClassTeacherRepository
    {
        public ClassTeacherRepository()
        {
            DefaultInclude = func => func.Include(teacher => teacher.User)
                .Include(teacher => teacher.Class);
        }

        public async Task<Entities.ClassTeacher?> GetByIdAsync(int id)
        {
            return await GetFirstOrDefaultAsync(x => x.Id == id);
        }

        public override async Task UpdateAsync(Entities.ClassTeacher entity)
        {
            var connectedTeacher = await DbContext.Set<Entities.ClassTeacher>().Include(x => x.User).FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (connectedTeacher == null)
            {
                throw new Exception();
            }

            var connectedUser = await DbContext.Set<User>().FirstOrDefaultAsync(x => x.Id == connectedTeacher.User.Id);

            if (connectedUser == null)
            {
                throw new Exception();
            }

            connectedUser.FirstName = entity.User.FirstName;
            connectedUser.LastName = entity.User.LastName;
            connectedUser.Patronymic = entity.User.Patronymic;

            DbContext.Entry(connectedTeacher).CurrentValues.SetValues(entity);

            DbContext.Entry(connectedTeacher).Reference(x => x.User).IsModified = false;

            await base.UpdateAsync(connectedTeacher);
        }
    }
}
