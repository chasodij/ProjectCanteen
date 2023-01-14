using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ProjectCanteen.BLL.DTOs.CanteenWorker;
using ProjectCanteen.BLL.Services.Interfaces;
using ProjectCanteen.DAL.Entities;
using ProjectCanteen.DAL.UnitOfWork;

namespace ProjectCanteen.BLL.Services.Implementations
{
    public class CanteenWorkerService : ICanteenWorkerService
    {
        private readonly IProjectCanteenUoW _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public CanteenWorkerService(IProjectCanteenUoW unitOfWork, IMapper mapper, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<bool> DeleteWorkerAsync(int id)
        {
            var worker = await _unitOfWork.CanteenWorkerRepository.GetByIdAsync(id);

            if (worker == null)
            {
                return false;
            }

            await _unitOfWork.CanteenWorkerRepository.DeleteAsync(worker);

            var user = await _userManager.FindByIdAsync(worker.User.Id);

            if (user != null)
            {
                await _unitOfWork.UserRepository.DeleteAsync(user);
            }

            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<CanteenWorker?> GetWorkerByUserId(string id)
        {
            var worker = await _unitOfWork.CanteenWorkerRepository.GetFirstOrDefaultAsync(x => x.User.Id == id);

            return worker;
        }

        public async Task<(bool isExist, int workerId)> GetWorkerIdByUserId(string id)
        {
            var worker = await _unitOfWork.CanteenWorkerRepository.GetFirstOrDefaultAsync(x => x.User.Id == id);

            return worker == null ? (false, 0) : (true, worker.Id);
        }

        public async Task<(IEnumerable<FullCanteenWorkerDTO> workers, int totalCount)> GetWorkersAsync(int page, int pageSize, int canteenId)
        {
            var workers = await _unitOfWork.CanteenWorkerRepository.GetRangeAsync(x => x.Canteen.Id == canteenId, page: page, pageSize: pageSize);

            return (_mapper.Map<IEnumerable<FullCanteenWorkerDTO>>(workers.entities), workers.totalCount);
        }

        public async Task UpdateWorkerAsync(UpdateCanteenWorkerDTO canteenWorkerDTO)
        {
            var workerFromDb = await _unitOfWork.CanteenWorkerRepository.GetFirstOrDefaultAsync(x => x.Id == canteenWorkerDTO.Id);

            if (workerFromDb == null)
            {
                throw new Exception();
            }

            var worker = _mapper.Map<CanteenWorker>(canteenWorkerDTO);

            await _unitOfWork.CanteenWorkerRepository.UpdateAsync(worker);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
