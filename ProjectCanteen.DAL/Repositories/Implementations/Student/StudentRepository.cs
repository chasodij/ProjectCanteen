using Microsoft.EntityFrameworkCore;
using ProjectCanteen.DAL.Entities;
using ProjectCanteen.DAL.Repositories.Implementations.NewFolder;
using ProjectCanteen.DAL.Repositories.Interfaces.Student;

namespace ProjectCanteen.DAL.Repositories.Implementations.Student
{
    public class StudentRepository : BaseRepository<Entities.Student>, IStudentRepository
    {
        public StudentRepository()
        {
            DefaultInclude = func => func
            .Include(student => student.User)
            .Include(student => student.Parents).ThenInclude(x => x.User)
            .Include(student => student.Class)
            .Include(student => student.DietaryRestrictions);
        }

        public async Task<Entities.Student?> GetByIdAsync(int id)
        {
            return await GetFirstOrDefaultAsync(x => x.Id == id);
        }

        public override async Task UpdateAsync(Entities.Student entity)
        {
            var connectedStudent = await DbContext.Set<Entities.Student>()
                .Include(x => x.Parents)
                .Include(x => x.DietaryRestrictions)
                .FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (connectedStudent == null)
            {
                throw new Exception();
            }

            var connectedUser = await DbContext.Set<User>().FirstOrDefaultAsync(x => x.Student != null && x.Student.Id == entity.Id);

            if (connectedUser == null)
            {
                throw new Exception();
            }

            connectedUser.FirstName = entity.User.FirstName;
            connectedUser.LastName = entity.User.LastName;
            connectedUser.Patronymic = entity.User.Patronymic;

            DbContext.Entry(connectedStudent).Reference(x => x.User).IsModified = false;

            DbContext.Entry(connectedStudent).CurrentValues.SetValues(entity);

            connectedStudent.Parents.Clear();
            foreach (var parent_to_add in entity.Parents)
            {
                var new_parent = DbContext.Parents
                    .FirstOrDefault(parent => parent.Id == parent_to_add.Id);
                if (new_parent != null)
                {
                    connectedStudent.Parents.Add(new_parent);
                }
            }

            connectedStudent.DietaryRestrictions.Clear();
            foreach (var restriction_to_add in entity.DietaryRestrictions)
            {
                var new_restriction = DbContext.DietaryRestrictions
                    .FirstOrDefault(restriction => restriction.Id == restriction_to_add.Id);
                if (new_restriction != null)
                {
                    connectedStudent.DietaryRestrictions.Add(new_restriction);
                }
            }

            var connectedClass = await DbContext.Set<Entities.Class>().FirstOrDefaultAsync(x => x.Id == entity.Class.Id);

            connectedStudent.Class = connectedClass;

            await base.UpdateAsync(connectedStudent);
        }

        public async Task UpdateRestrictionsAsync(Entities.Student student)
        {
            var connectedStudent = await DbContext.Set<Entities.Student>()
                .Include(x => x.DietaryRestrictions)
                .FirstOrDefaultAsync(x => x.Id == student.Id);

            if (connectedStudent == null)
            {
                throw new Exception();
            }

            connectedStudent.DietaryRestrictions.Clear();
            foreach (var restriction_to_add in student.DietaryRestrictions)
            {
                var new_restriction = DbContext.DietaryRestrictions
                    .FirstOrDefault(restriction => restriction.Id == restriction_to_add.Id);
                if (new_restriction != null)
                {
                    connectedStudent.DietaryRestrictions.Add(new_restriction);
                }
            }

            await base.UpdateAsync(connectedStudent);
        }

        public async Task UpdateTagAsync(Entities.Student student)
        {
            var connectedStudent = await DbContext.Set<Entities.Student>()
                .FirstOrDefaultAsync(x => x.Id == student.Id);

            if (connectedStudent == null)
            {
                throw new Exception();
            }

            connectedStudent.TagId = student.TagId;

            await base.UpdateAsync(connectedStudent);
        }
    }
}
