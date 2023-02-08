using ProjectCanteen.BLL.DTOs.DietaryRestriction;

namespace ProjectCanteen.BLL.DTOs.Ingredient
{
    public class FullIngredientDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double ProteinsPer100g { get; set; }
        public double FatsPer100g { get; set; }
        public double CarbohydratesPer100g { get; set; }
        public int CaloriesPer100g { get; set; }
        public int? CanteenId { get; set; }
        public string? CanteenName { get; set; }
        public List<DietaryRestrictionDTO> DietaryRestrictions { get; set; }
    }
}
