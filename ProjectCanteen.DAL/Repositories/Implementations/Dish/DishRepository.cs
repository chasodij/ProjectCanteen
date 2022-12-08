using Microsoft.EntityFrameworkCore;
using ProjectCanteen.DAL.Repositories.Implementations.NewFolder;
using ProjectCanteen.DAL.Repositories.Interfaces.Dish;

namespace ProjectCanteen.DAL.Repositories.Implementations.Dish
{
    public class DishRepository : BaseRepository<Entities.Dish>, IDishRepository
    {
        public async Task<Entities.Dish?> GetByIdAsync(int id)
        {
            return await GetFirstOrDefaultAsync(x => x.Id == id);
        }

        public async override Task UpdateAsync(Entities.Dish entity)
        {
            var connected_entity = DbContext.Dishes.FirstOrDefault(x => x.Id == entity.Id);

            if (connected_entity == null)
            {
                throw new Exception();
            }

            DbContext.Entry(connected_entity).CurrentValues.SetValues(entity);

            var items_from_context = DbContext.IngredientInDishes.Where(x => x.DishId == entity.Id);

            foreach (var item in items_from_context)
            {
                if (!entity.IngredientInDishes.Exists(x => x.IngredientId == item.IngredientId))
                {
                    DbContext.Entry(item).State = EntityState.Deleted;
                }
            }

            foreach (var ingredient_to_add in entity.IngredientInDishes)
            {
                var connected_ingredient = await DbContext.IngredientInDishes.FirstOrDefaultAsync(x =>
                    x.IngredientId == ingredient_to_add.IngredientId && x.DishId == entity.Id);

                if (connected_ingredient != null)
                {
                    connected_ingredient.AmountInGrams = ingredient_to_add.AmountInGrams;
                }

                else
                {
                    DbContext.IngredientInDishes.Add(ingredient_to_add);
                }
            }

            await base.UpdateAsync(connected_entity);
        }
    }
}
