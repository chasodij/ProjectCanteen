namespace ProjectCanteen.DAL.Entities
{
    public class MenuOfTheDay
    {
        public int Id { get; set; }
        public Canteen Canteen { get; set; }
        public DateTime Day { get; set; }
        public List<Dish> Dishes { get; set; } = new List<Dish>();
    }
}
