using ProjectCanteen.BLL.DTOs.MenuSection;

namespace ProjectCanteen.BLL.Services.Interfaces
{
    public interface IMenuSectionService
    {
        Task<(IEnumerable<MenuSectionDTO> menuSections, int totalCount)> GetMenuSectionsAsync(int page, int pageSize);
        Task CreateMenuSectionAsync(CreateMenuSectionDTO createMenuSectionDTO);
        Task UpdateMenuSectionAsync(MenuSectionDTO menuSectionDTO);
        Task<bool> IsMenuSectionExistWithIdAsync(int id);
        Task<bool> DeleteMenuSectionAsync(int id);
    }
}
