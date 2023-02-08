using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectCanteen.BLL.DTOs.Parent;
using ProjectCanteen.BLL.Services.Interfaces;
using ProjectCanteen.DAL.Entities;
using ProjectCanteen.DAL.UnitOfWork;

namespace ProjectCanteen.BLL.Services.Implementations
{
    public class SchoolAdminService : ISchoolAdminService
    {
        private readonly IProjectCanteenUoW _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public SchoolAdminService(IProjectCanteenUoW unitOfWork, IMapper mapper, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<bool> DeleteSchoolAdminAsync(int id)
        {
            var admin = await _unitOfWork.SchoolAdminRepository.GetFirstOrDefaultAsync(x => x.Id == id);

            if (admin == null)
            {
                return false;
            }

            await _unitOfWork.SchoolAdminRepository.DeleteAsync(admin);

            var user = await _userManager.FindByIdAsync(admin.User.Id);

            if (user != null)
            {
                await _unitOfWork.UserRepository.DeleteAsync(user);
            }

            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<SchoolAdmin?> GetSchoolAdminByUserId(string id)
        {
            var admin = await _unitOfWork.SchoolAdminRepository.GetFirstOrDefaultAsync(x => x.User.Id == id);

            return admin;
        }

        public async Task<(IEnumerable<FullParentDTO> admins, int totalCount)> GetSchoolAdminsAsync(int page, int pageSize)
        {
            var parents = await _unitOfWork.ParentRepository.GetRangeAsync(page: page, pageSize: pageSize);

            return (_mapper.Map<IEnumerable<FullParentDTO>>(parents.entities), parents.totalCount);
        }

        public async Task UpdateSchoolAdminAsync(UpdateParentDTO parentDTO)
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
