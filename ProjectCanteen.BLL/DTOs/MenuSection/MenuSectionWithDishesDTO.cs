using ProjectCanteen.BLL.DTOs.Dish;

namespace ProjectCanteen.BLL.DTOs.MenuSection
{
    public class MenuSectionWithDishesDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberInMenu { get; set; }
        public List<ShortDishDTO> Dishes { get; set; }
    }
}
