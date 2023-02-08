using Microsoft.EntityFrameworkCore;
using ProjectCanteen.DAL.Repositories.Implementations.NewFolder;
using ProjectCanteen.DAL.Repositories.Interfaces.Canteen;

namespace ProjectCanteen.DAL.Repositories.Implementations.Canteen
{
    public class CanteenRepository : BaseRepository<Entities.Canteen>, ICanteenRepository
    {
        public CanteenRepository()
        {
            DefaultInclude = func => func.Include(x => x.Ingredients).ThenInclude(x => x.DietaryRestrictions)
                                          .Include(x => x.School);
        }
    }
}
