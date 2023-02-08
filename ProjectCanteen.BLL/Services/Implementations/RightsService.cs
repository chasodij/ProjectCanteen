using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectCanteen.BLL.DTOs.Authentication;
using ProjectCanteen.BLL.Services.Interfaces;
using ProjectCanteen.DAL.Entities;
using ProjectCanteen.DAL.UnitOfWork;

namespace ProjectCanteen.BLL.Services.Implementations
{
    public class RightsService : IRightsService
    {
        private readonly IProjectCanteenUoW _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public RightsService(IProjectCanteenUoW unitOfWork,
            IMapper mapper,
            UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<bool> HasUserRightsToChange(User user, SignUpTerminalDTO terminalDTO)
        {
            var roles = await _userManager.GetRolesAsync(user);
            if (IsAdmin(roles))
            {
                return true;
            }

            if (!isSchoolAdmin(roles))
            {
                return false;
            }

            var schoolAdmin = await GetSchoolAdmin(user);
            if (schoolAdmin == null)
            {
                return false;
            }

            var canteen = await _unitOfWork.CanteenRepository.GetFirstOrDefaultAsync(x => x.Id == terminalDTO.CanteenId);
            if (canteen == null)
            {
                return false;
            }

            return schoolAdmin.School.Id == canteen.School.Id;
        }

        public async Task<bool> HasUserRightsToChange(User user, SignUpCanteenWorkerDTO canteenWorkerDTO)
        {
            var roles = await _userManager.GetRolesAsync(user);
            if (IsAdmin(roles))
            {
                return true;
            }

            if (!isSchoolAdmin(roles))
            {
                return false;
            }

            var schoolAdmin = await GetSchoolAdmin(user);
            if (schoolAdmin == null)
            {
                return false;
            }

            var canteen = await _unitOfWork.CanteenRepository.GetFirstOrDefaultAsync(x => x.Id == canteenWorkerDTO.CanteenId);
            if (canteen == null)
            {
                return false;
            }

            return schoolAdmin.School.Id == canteen.School.Id;
        }

        public async Task<bool> HasUserRightsToChange(User user, SignUpClassTeacherDTO classTeacherDTO)
        {
            var roles = await _userManager.GetRolesAsync(user);
            if (IsAdmin(roles))
            {
                return true;
            }

            if (!isSchoolAdmin(roles))
            {
                return false;
            }

            var schoolAdmin = await GetSchoolAdmin(user);
            if (schoolAdmin == null)
            {
                return false;
            }

            var cur_class = await _unitOfWork.ClassRepository.GetFirstOrDefaultAsync(x => x.Id == classTeacherDTO.ClassId);
            if (cur_class == null)
            {
                return false;
            }

            return schoolAdmin.School.Id == cur_class.School.Id;
        }

        public async Task<bool> HasUserRightsToChange(User user, SignUpParentDTO parentDTO)
        {
            var roles = await _userManager.GetRolesAsync(user);
            if (IsAdmin(roles))
            {
                return true;
            }

            if (!isSchoolAdmin(roles))
            {
                return false;
            }

            var schoolAdmin = await GetSchoolAdmin(user);
            if (schoolAdmin == null)
            {
                return false;
            }

            var students = await _unitOfWork.StudentRepository.GetAllAsync(x => parentDTO.ChildrenId.Contains(x.Id));
            if (students.Count() == 0)
            {
                return false;
            }

            return students.All(x => x.Class.School.Id == schoolAdmin.School.Id);
        }

        public async Task<bool> HasUserRightsToChange(User user, SignUpStudentDTO studentDTO)
        {
            var roles = await _userManager.GetRolesAsync(user);
            if (IsAdmin(roles))
            {
                return true;
            }

            if (!isSchoolAdmin(roles))
            {
                return false;
            }

            var schoolAdmin = await GetSchoolAdmin(user);
            if (schoolAdmin == null)
            {
                return false;
            }

            var cur_class = await _unitOfWork.ClassRepository.GetFirstOrDefaultAsync(x => x.Id == studentDTO.ClassId);
            if (cur_class == null)
            {
                return false;
            }

            return schoolAdmin.School.Id == cur_class.School.Id;
        }

        public async Task<bool> HasUserRightsToChangeIngredient(User user, int ingredientId)
        {
            var roles = await _userManager.GetRolesAsync(user);

            if (IsAdmin(roles))
            {
                var ingredient = await _unitOfWork.IngredientRepository.GetFirstOrDefaultAsync(x => x.Id == ingredientId);

                if (ingredient == null)
                {
                    throw new Exception();
                }

                return ingredient.CanteenId == null;
            }

            if (isCanteenWorker(roles))
            {
                var canteenWorker = await GetCanteenWorker(user);

                if (canteenWorker == null)
                {
                    throw new Exception();
                }

                var ingredient = await _unitOfWork.IngredientRepository.GetFirstOrDefaultAsync(x => x.Id == ingredientId);

                if (ingredient == null)
                {
                    throw new Exception();
                }

                return canteenWorker.Canteen.Id == ingredient.CanteenId;
            }

            return false;
        }

        public async Task<bool> HasUserRightsToUseIngredient(User user, int ingredientId)
        {
            var roles = await _userManager.GetRolesAsync(user);

            if (isCanteenWorker(roles))
            {
                var canteenWorker = await GetCanteenWorker(user);

                if (canteenWorker == null)
                {
                    throw new Exception();
                }

                var ingredient = await _unitOfWork.IngredientRepository.GetFirstOrDefaultAsync(x => x.Id == ingredientId);

                if (ingredient == null)
                {
                    throw new Exception();
                }

                return canteenWorker.Canteen.Id == ingredient.CanteenId || ingredient.CanteenId == null;
            }

            return false;
        }

        public async Task<bool> HasUserRightsToChangeDish(User user, int dishId)
        {
            var canteenWorker = await GetCanteenWorker(user);

            if (canteenWorker == null)
            {
                throw new Exception();
            }

            var dish = await _unitOfWork.DishRepository.GetFirstOrDefaultAsync(x => x.Id == dishId);

            if (dish == null)
            {
                throw new Exception();
            }

            return canteenWorker.Canteen.Id == dish.Canteen.Id;
        }

        public async Task<bool> HasUserRightsToChangeMenu(User user, int menuId)
        {
            var canteenWorker = await GetCanteenWorker(user);

            if (canteenWorker == null)
            {
                throw new Exception();
            }

            var menu = await _unitOfWork.MenuOfTheDayRepository.GetFirstOrDefaultAsync(x => x.Id == menuId);

            if (menu == null)
            {
                throw new Exception();
            }

            return canteenWorker.Canteen.Id == menu.Canteen.Id;
        }

        public async Task<bool> HasUserRightsToChangeStudent(User user, int studentId, int canteenId)
        {
            var canteenWorker = await GetCanteenWorker(user);

            if (canteenWorker != null)
            {
                var cur_class = await _unitOfWork.ClassRepository.GetFirstOrDefaultAsync(x => x.Students.Any(x => x.Id == studentId),
                    include: func => func.Include(x => x.School).ThenInclude(x => x.Canteens));

                if (cur_class == null)
                {
                    throw new Exception();
                }

                return canteenWorker.Canteen.School.Id == canteenId && cur_class.School.Canteens.Any(x => x.Id == canteenId);
            }

            var parent = await GetParent(user);

            if (parent != null)
            {
                var cur_class = await _unitOfWork.ClassRepository.GetFirstOrDefaultAsync(x => x.Students.Any(x => x.Id == studentId),
                    include: func => func.Include(x => x.School).ThenInclude(x => x.Canteens));

                if (cur_class == null)
                {
                    throw new Exception();
                }

                return parent.Children.Any(x => x.Id == studentId) && cur_class.School.Canteens.Any(x => x.Id == canteenId);
            }

            return false;
        }

        private bool IsAdmin(IEnumerable<string> roles)
        {
            return roles.Contains(Roles.Admin);
        }

        private bool isSchoolAdmin(IEnumerable<string> roles)
        {
            return roles.Contains(Roles.SchoolAdmin);
        }

        private async Task<SchoolAdmin?> GetSchoolAdmin(User user)
        {
            var schoolAdmin = await _unitOfWork.SchoolAdminRepository.GetFirstOrDefaultAsync(x => x.User.Id == user.Id);

            return schoolAdmin;
        }

        private bool isCanteenWorker(IEnumerable<string> roles)
        {
            return roles.Contains(Roles.CanteenWorker);
        }

        private async Task<CanteenWorker?> GetCanteenWorker(User user)
        {
            var canteenWorker = await _unitOfWork.CanteenWorkerRepository.GetFirstOrDefaultAsync(x => x.User.Id == user.Id);

            return canteenWorker;
        }

        private bool isParent(IEnumerable<string> roles)
        {
            return roles.Contains(Roles.Parent);
        }

        private async Task<Parent?> GetParent(User user)
        {
            var parent = await _unitOfWork.ParentRepository.GetFirstOrDefaultAsync(x => x.User.Id == user.Id);

            return parent;
        }

        public Task<bool> HasUserRightsToChangeCanteen(User user, int canteenId)
        {
            throw new NotImplementedException();
        }
    }
}
