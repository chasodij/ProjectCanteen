namespace ProjectCanteen.BLL.DTOs.MenuOfTheDay
{
    public class CreateMenuOfTheDayDTO
    {
        public DateTime Day { get; set; }
        public List<int> DishesId { get; set; }
    }
}
