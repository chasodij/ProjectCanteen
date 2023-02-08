namespace ProjectCanteen.DAL.Entities
{
    public class MenuSection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberInMenu { get; set; }
        public List<Dish> Dishes { get; set; }
    }
}
