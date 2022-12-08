namespace ProjectCanteen.BLL.DTOs.MenuOfTheDay
{
    public class MenuOfTheDayDTO
    {
        public int Id { get; set; }
        public DateTime Day { get; set; }
        public List<int> DishesId { get; set; }
    }
}
