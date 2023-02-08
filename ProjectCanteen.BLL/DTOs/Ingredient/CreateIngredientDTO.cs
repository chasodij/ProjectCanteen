namespace ProjectCanteen.BLL.DTOs.Ingredient
{
    public class CreateIngredientDTO
    {
        public string Name { get; set; }
        public double ProteinsPer100g { get; set; }
        public double FatsPer100g { get; set; }
        public double CarbohydratesPer100g { get; set; }
        public int CaloriesPer100g { get; set; }
        public List<int> DietaryRestrictionsId { get; set; }
    }
}
