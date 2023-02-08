namespace ProjectCanteen.DAL.Entities
{
    public class DietaryRestriction
    {
        public int Id { get; set; }
        public string ShortTitle { get; set; }
        public string Description { get; set; }
        public List<Ingredient> Ingredients { get; set; }
    }
}
