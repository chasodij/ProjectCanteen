using AutoMapper;
using ProjectCanteen.BLL.DTOs.MenuSection;
using ProjectCanteen.BLL.Services.Interfaces;
using ProjectCanteen.DAL.Entities;
using ProjectCanteen.DAL.UnitOfWork;

namespace ProjectCanteen.BLL.Services.Implementations
{
    public class MenuSectionService : IMenuSectionService
    {
        private readonly IProjectCanteenUoW _unitOfWork;
        private readonly IMapper _mapper;

        public MenuSectionService(IProjectCanteenUoW unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateMenuSectionAsync(CreateMenuSectionDTO createMenuSectionDTO)
        {
            var section = _mapper.Map<MenuSection>(createMenuSectionDTO);

            await _unitOfWork.MenuSectionRepository.AttachAsync(section);
            await _unitOfWork.MenuSectionRepository.CreateAsync(section);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> DeleteMenuSectionAsync(int id)
        {
            var section = await _unitOfWork.MenuSectionRepository.GetFirstOrDefaultAsync(x => x.Id == id);

            if (section != null)
            {
                await _unitOfWork.MenuSectionRepository.DeleteAsync(section);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<(IEnumerable<MenuSectionDTO> menuSections, int totalCount)> GetMenuSectionsAsync(int page, int pageSize)
        {
            var sections = await _unitOfWork.MenuSectionRepository.GetRangeAsync(page: page, pageSize: pageSize);

            return (_mapper.Map<IEnumerable<MenuSectionDTO>>(sections.entities), sections.totalCount);
        }

        public async Task<bool> IsMenuSectionExistWithIdAsync(int id)
        {
            var section = await _unitOfWork.MenuSectionRepository.GetFirstOrDefaultAsync(x => x.Id == id);

            return section != null;
        }

        public async Task UpdateMenuSectionAsync(MenuSectionDTO menuSectionDTO)
        {
            var section = _mapper.Map<MenuSection>(menuSectionDTO);

            await _unitOfWork.MenuSectionRepository.AttachAsync(section);

            await _unitOfWork.MenuSectionRepository.UpdateAsync(section);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
