namespace ProjectCanteen.DAL.Entities
{
    public class MenuOfTheDay
    {
        public int Id { get; set; }
        public DateTime Day { get; set; }
        public Canteen Canteen { get; set; }
        public List<Dish> Dishes { get; set; }
        public bool IsCreatedOrUpdatedLate { get; set; }
        public List<Order> Orders { get; set; }
    }
}
