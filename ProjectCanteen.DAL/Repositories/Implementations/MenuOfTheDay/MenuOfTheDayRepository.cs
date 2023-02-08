using Microsoft.EntityFrameworkCore;
using ProjectCanteen.DAL.Repositories.Implementations.NewFolder;
using ProjectCanteen.DAL.Repositories.Interfaces.MenuOfTheDay;

namespace ProjectCanteen.DAL.Repositories.Implementations.MenuOfTheDay
{
    public class MenuOfTheDayRepository : BaseRepository<Entities.MenuOfTheDay>, IMenuOfTheDayRepository
    {
        public MenuOfTheDayRepository()
        {
            DefaultInclude = func => func.Include(ingredient => ingredient.Canteen)
                .Include(ingredient => ingredient.Dishes);
        }

        public async Task<Entities.MenuOfTheDay?> GetByIdAsync(int id)
        {
            return await GetFirstOrDefaultAsync(x => x.Id == id);
        }

        public override async Task UpdateAsync(Entities.MenuOfTheDay entity)
        {
            var connected_entity = DefaultInclude(DbContext.MenuOfTheDays).FirstOrDefault(x => x.Id == entity.Id);

            if (connected_entity == null)
            {
                throw new Exception();
            }

            TimeSpan span = entity.Day.Subtract(DateTime.UtcNow);

            if (span.TotalHours <= connected_entity.Canteen.MinHoursToCreateMenu)
            {
                entity.IsCreatedOrUpdatedLate = true;
            }

            DbContext.Entry(connected_entity).CurrentValues.SetValues(entity);

            connected_entity.Dishes.Clear();
            foreach (var dish_to_add in entity.Dishes)
            {
                var new_dish = DbContext.Dishes
                    .FirstOrDefault(dish => dish.Id == dish_to_add.Id);
                if (new_dish != null)
                {
                    connected_entity.Dishes.Add(new_dish);
                }
            }

            await base.UpdateAsync(connected_entity);
        }
    }
}
