using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectCanteen.BLL.DTOs.Canteen;
using ProjectCanteen.BLL.Services.Interfaces;
using ProjectCanteen.DAL.Entities;
using ProjectCanteen.DAL.UnitOfWork;

namespace ProjectCanteen.BLL.Services.Implementations
{
    public class CanteenService : ICanteenService
    {
        private readonly IProjectCanteenUoW _unitOfWork;
        private readonly IMapper _mapper;

        public CanteenService(IProjectCanteenUoW unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateCanteenAsync(CreateCanteenDTO createCanteenDTO)
        {
            var canteen = _mapper.Map<Canteen>(createCanteenDTO);

            await _unitOfWork.CanteenRepository.AttachAsync(canteen);
            await _unitOfWork.CanteenRepository.CreateAsync(canteen);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> DeleteCanteenAsync(int id)
        {
            var canteen = await _unitOfWork.CanteenRepository.GetFirstOrDefaultAsync(x => x.Id == id);

            if (canteen != null)
            {
                await _unitOfWork.CanteenRepository.DeleteAsync(canteen);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<(IEnumerable<FullCanteenDTO> canteens, int totalCount)> GetCanteensAsync(int page, int pageSize, int schoolId)
        {
            var canteens = await _unitOfWork.CanteenRepository.GetRangeAsync(x => x.School.Id == schoolId, page: page, pageSize: pageSize,
                include: func => func.Include(x => x.School).Include(x => x.Terminal));

            return (_mapper.Map<IEnumerable<FullCanteenDTO>>(canteens.entities), canteens.totalCount);
        }

        public async Task<bool> IsCanteenExistWithIdAsync(int id)
        {
            var canteen = await _unitOfWork.CanteenRepository.GetFirstOrDefaultAsync(x => x.Id == id);

            return canteen != null;
        }

        public async Task UpdateCanteenAsync(UpdateCanteenDTO canteenDTO)
        {
            var canteen = _mapper.Map<Canteen>(canteenDTO);

            await _unitOfWork.CanteenRepository.AttachAsync(canteen);

            await _unitOfWork.CanteenRepository.UpdateAsync(canteen);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
