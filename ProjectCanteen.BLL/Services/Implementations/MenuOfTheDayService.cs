using AutoMapper;
using ProjectCanteen.BLL.DTOs.MenuOfTheDay;
using ProjectCanteen.BLL.Services.Interfaces;
using ProjectCanteen.DAL.Entities;
using ProjectCanteen.DAL.UnitOfWork;

namespace ProjectCanteen.BLL.Services.Implementations
{
    public class MenuOfTheDayService : IMenuOfTheDayService
    {
        private readonly IProjectCanteenUoW _unitOfWork;
        private readonly IMapper _mapper;

        public MenuOfTheDayService(IProjectCanteenUoW unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateMenuOfTheDayAsync(CreateMenuOfTheDayDTO createMenuOfTheDayDTO)
        {
            var menu = _mapper.Map<MenuOfTheDay>(createMenuOfTheDayDTO);

            await _unitOfWork.MenuOfTheDayRepository.AttachAsync(menu);
            await _unitOfWork.MenuOfTheDayRepository.CreateAsync(menu);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> DeleteMenuOfTheDayAsync(int id)
        {
            var menu = await _unitOfWork.MenuOfTheDayRepository.GetByIdAsync(id);

            if (menu != null)
            {
                await _unitOfWork.MenuOfTheDayRepository.DeleteAsync(menu);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<MenuOfTheDayDTO>> GetMenuOfTheDayAsync()
        {
            var menus = await _unitOfWork.MenuOfTheDayRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<MenuOfTheDayDTO>>(menus);
        }

        public async Task<bool> IsMenuOfTheDayExistWithIdAsync(int id)
        {
            var menu = await _unitOfWork.MenuOfTheDayRepository.GetByIdAsync(id);

            return menu != null;
        }

        public async Task UpdateMenuOfTheDayAsync(MenuOfTheDayDTO menuOfTheDayDTO)
        {
            var menu = _mapper.Map<MenuOfTheDay>(menuOfTheDayDTO);

            await _unitOfWork.MenuOfTheDayRepository.UpdateAsync(menu);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
