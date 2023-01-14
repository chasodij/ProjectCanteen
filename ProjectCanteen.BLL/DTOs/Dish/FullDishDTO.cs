using ProjectCanteen.BLL.DTOs.IngredientInDish;
using ProjectCanteen.BLL.DTOs.MenuSection;

namespace ProjectCanteen.BLL.DTOs.Dish
{
    public class FullDishDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public MenuSectionDTO MenuSection { get; set; }
        public List<IngredientInDishDTO> IngredientsInDish { get; set; }
    }
}
