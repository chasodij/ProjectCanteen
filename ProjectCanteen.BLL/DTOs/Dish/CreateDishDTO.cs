using ProjectCanteen.BLL.DTOs.IngredientInDish;

namespace ProjectCanteen.BLL.DTOs.Dish
{
    public class CreateDishDTO
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int MenuSectionId { get; set; }
        public List<AddIngredientToDishDTO> IngredientsInDish { get; set; }
    }
}
