namespace ProjectCanteen.DAL.Entities
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double ProteinsPer100g { get; set; }
        public double FatsPer100g { get; set; }
        public double CarbohydratesPer100g { get; set; }
        public int CaloriesPer100g { get; set; }
        public int? CanteenId { get; set; }
        public Canteen? Canteen { get; set; }
        public List<DietaryRestriction> DietaryRestrictions { get; set; }
    }
}
