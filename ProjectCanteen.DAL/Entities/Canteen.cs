namespace ProjectCanteen.DAL.Entities
{
    public class Canteen
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public School School { get; set; }
        public User? Terminal { get; set; }
        public string? TerminalId { get; set; }
        public int? MinHoursToCreateMenu { get; set; }
        public int? MinHoursToOrder { get; set; }
        public decimal? MaxStudentDebt { get; set; }
        public List<CanteenWorker> CanteenWorkers { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<Dish> Dishes { get; set; }
    }
}
