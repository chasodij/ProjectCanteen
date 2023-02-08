using ProjectCanteen.BLL.DTOs.MenuSection;

namespace ProjectCanteen.BLL.DTOs.MenuOfTheDay
{
    public class FullMenuOfTheDayDTO
    {
        public int Id { get; set; }
        public DateTime Day { get; set; }
        public bool IsCreatedOrUpdatedLate { get; set; }
        public List<MenuSectionWithDishesDTO> MenuSections { get; set; }
    }
}
