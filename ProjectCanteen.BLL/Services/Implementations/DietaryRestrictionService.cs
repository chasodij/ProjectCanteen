using AutoMapper;
using ProjectCanteen.BLL.DTOs.DietaryRestriction;
using ProjectCanteen.BLL.Services.Interfaces;
using ProjectCanteen.DAL.Entities;
using ProjectCanteen.DAL.UnitOfWork;

namespace ProjectCanteen.BLL.Services.Implementations
{
    public class DietaryRestrictionService : IDietaryRestrictionService
    {
        private readonly IProjectCanteenUoW _unitOfWork;
        private readonly IMapper _mapper;

        public DietaryRestrictionService(IProjectCanteenUoW unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateDietaryRestrictionAsync(CreateDietaryRestrictionDTO createDietaryRestrictionDTO)
        {
            var restriction = _mapper.Map<DietaryRestriction>(createDietaryRestrictionDTO);

            await _unitOfWork.DietaryRestrictionRepository.AttachAsync(restriction);
            await _unitOfWork.DietaryRestrictionRepository.CreateAsync(restriction);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> DeleteDietaryRestrictionAsync(int id)
        {
            var restriction = await _unitOfWork.DietaryRestrictionRepository.GetFirstOrDefaultAsync(x => x.Id == id);

            if (restriction != null)
            {
                await _unitOfWork.DietaryRestrictionRepository.DeleteAsync(restriction);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<(IEnumerable<DietaryRestrictionDTO> restrictions, int totalCount)> GetDietaryRestrictionsAsync(int page, int pageSize)
        {

            var (restrictions, totalCount) = await _unitOfWork.DietaryRestrictionRepository.GetRangeAsync(page: page, pageSize: pageSize);

            return (_mapper.Map<IEnumerable<DietaryRestrictionDTO>>(restrictions), totalCount);
        }

        public async Task<IEnumerable<DietaryRestrictionDTO>> GetDietaryRestrictionsOfStudentAsync(int id)
        {
            var student = await _unitOfWork.StudentRepository.GetFirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<IEnumerable<DietaryRestrictionDTO>>(student.DietaryRestrictions);
        }

        public async Task<bool> IsDietaryRestrictionExistWithIdAsync(int id)
        {
            var restriction = await _unitOfWork.DietaryRestrictionRepository.GetFirstOrDefaultAsync(x => x.Id == id);

            return restriction != null;
        }

        public async Task UpdateDietaryRestrictionAsync(DietaryRestrictionDTO dietaryRestrictionDTO)
        {
            var restriction = _mapper.Map<DietaryRestriction>(dietaryRestrictionDTO);

            await _unitOfWork.DietaryRestrictionRepository.AttachAsync(restriction);

            await _unitOfWork.DietaryRestrictionRepository.UpdateAsync(restriction);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
