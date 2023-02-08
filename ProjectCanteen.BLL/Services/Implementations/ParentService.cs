using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ProjectCanteen.BLL.DTOs.Parent;
using ProjectCanteen.BLL.Services.Interfaces;
using ProjectCanteen.DAL.Entities;
using ProjectCanteen.DAL.UnitOfWork;

namespace ProjectCanteen.BLL.Services.Implementations
{
    public class ParentService : IParentService
    {
        private readonly IProjectCanteenUoW _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public ParentService(IProjectCanteenUoW unitOfWork, IMapper mapper, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<bool> DeleteParentAsync(int id)
        {
            var parent = await _unitOfWork.ParentRepository.GetByIdAsync(id);

            if (parent == null)
            {
                return false;
            }

            await _unitOfWork.ParentRepository.DeleteAsync(parent);

            var user = await _userManager.FindByIdAsync(parent.User.Id);

            if (user != null)
            {
                await _unitOfWork.UserRepository.DeleteAsync(user);
            }

            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<Parent?> GetParentByUserId(string id)
        {
            var parent = await _unitOfWork.ParentRepository.GetFirstOrDefaultAsync(x => x.User.Id == id);

            return parent;
        }

        public async Task<(IEnumerable<FullParentDTO> parents, int totalCount)> GetParentsAsync(int page, int pageSize)
        {
            var parents = await _unitOfWork.ParentRepository.GetRangeAsync(page: page, pageSize: pageSize);

            return (_mapper.Map<IEnumerable<FullParentDTO>>(parents.entities), parents.totalCount);
        }

        public async Task UpdateParentAsync(UpdateParentDTO parentDTO)
        {
            var parentFromDb = await _unitOfWork.ParentRepository.GetByIdAsync(parentDTO.Id);

            if (parentFromDb == null)
            {
                throw new Exception();
            }

            var parent = _mapper.Map<Parent>(parentDTO);

            await _unitOfWork.ParentRepository.UpdateAsync(parent);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
