using ProjectCanteen.BLL.DTOs.DietaryRestriction;

namespace ProjectCanteen.BLL.Services.Interfaces
{
    public interface IDietaryRestrictionService
    {
        Task<(IEnumerable<DietaryRestrictionDTO> restrictions, int totalCount)> GetDietaryRestrictionsAsync(int page, int pageSize);
        Task<IEnumerable<DietaryRestrictionDTO>> GetDietaryRestrictionsOfStudentAsync(int id);
        Task CreateDietaryRestrictionAsync(CreateDietaryRestrictionDTO createDietaryRestrictionDTO);
        Task UpdateDietaryRestrictionAsync(DietaryRestrictionDTO dietaryRestrictionDTO);
        Task<bool> IsDietaryRestrictionExistWithIdAsync(int id);
        Task<bool> DeleteDietaryRestrictionAsync(int id);
    }
}
