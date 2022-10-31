namespace ProjectCanteen.DAL.Entities
{
    public class DietaryRestriction
    {
        public int Id { get; set; }
        public string ShortTitle { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    }
}
