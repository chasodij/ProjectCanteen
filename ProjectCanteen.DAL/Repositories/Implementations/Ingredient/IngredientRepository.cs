﻿using Microsoft.EntityFrameworkCore;
using ProjectCanteen.DAL.Repositories.Implementations.NewFolder;
using ProjectCanteen.DAL.Repositories.Interfaces.Ingredient;

namespace ProjectCanteen.DAL.Repositories.Implementations.Ingredient
{
    public class IngredientRepository : BaseRepository<Entities.Ingredient>, IIngredientRepository
    {
        public IngredientRepository()
        {
            DefaultInclude = func => func.Include(ingredient => ingredient.Canteen)
                .Include(ingredient => ingredient.DietaryRestrictions);
        }

        public async Task<Entities.Ingredient?> GetByIdAsync(int id)
        {
            return await GetFirstOrDefaultAsync(x => x.Id == id);
        }

        public override async Task UpdateAsync(Entities.Ingredient entity)
        {
            var connected_entity = DefaultInclude(DbContext.Ingredients).FirstOrDefault(x => x.Id == entity.Id);

            if (connected_entity == null)
            {
                throw new Exception();
            }

            DbContext.Entry(connected_entity).CurrentValues.SetValues(entity);

            DbContext.Entry(connected_entity).Reference(x => x.Canteen).IsModified = false;

            connected_entity.DietaryRestrictions.Clear();
            foreach (var restriction_to_add in entity.DietaryRestrictions)
            {
                var new_restriction = DbContext.DietaryRestrictions
                    .FirstOrDefault(restriction => restriction.Id == restriction_to_add.Id);
                if (new_restriction != null)
                {
                    connected_entity.DietaryRestrictions.Add(new_restriction);
                }
            }

            await base.UpdateAsync(connected_entity);
        }
    }
}
