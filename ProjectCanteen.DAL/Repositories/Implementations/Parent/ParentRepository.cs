using Microsoft.EntityFrameworkCore;
using ProjectCanteen.DAL.Entities;
using ProjectCanteen.DAL.Repositories.Implementations.NewFolder;
using ProjectCanteen.DAL.Repositories.Interfaces.Parent;

namespace ProjectCanteen.DAL.Repositories.Implementations.Parent
{
    public class ParentRepository : BaseRepository<Entities.Parent>, IParentRepository
    {
        public ParentRepository()
        {
            DefaultInclude = func => func.Include(parent => parent.User)
                .Include(parent => parent.Children).ThenInclude(x => x.User);
        }

        public async Task<Entities.Parent?> GetByIdAsync(int id)
        {
            return await GetFirstOrDefaultAsync(x => x.Id == id);
        }

        public override async Task UpdateAsync(Entities.Parent entity)
        {
            var connectedParent = await DbContext.Set<Entities.Parent>().Include(x => x.Children)
                .FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (connectedParent == null)
            {
                throw new Exception();
            }

            var connectedUser = await DbContext.Set<User>().FirstOrDefaultAsync(x => x.Parent != null && x.Parent.Id == entity.Id);

            if (connectedUser == null)
            {
                throw new Exception();
            }

            connectedUser.FirstName = entity.User.FirstName;
            connectedUser.LastName = entity.User.LastName;
            connectedUser.Patronymic = entity.User.Patronymic;

            DbContext.Entry(connectedParent).Reference(x => x.User).IsModified = false;

            connectedParent.Children.Clear();
            foreach (var child_to_add in entity.Children)
            {
                var new_child = DbContext.Students
                    .FirstOrDefault(student => student.Id == child_to_add.Id);
                if (new_child != null)
                {
                    connectedParent.Children.Add(new_child);
                }
            }

            await base.UpdateAsync(connectedParent);
        }
    }
}