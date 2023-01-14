using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using ProjectCanteen.BLL.DTOs.Authentication;
using ProjectCanteen.BLL.DTOs.Base;
using ProjectCanteen.BLL.Services.Interfaces;
using ProjectCanteen.DAL.Entities;
using ProjectCanteen.DAL.UnitOfWork;

namespace ProjectCanteen.BLL.Services.Implementations
{
    public class AuthentificationService : IAuthentificationService
    {
        private readonly UserManager<User> _userManager;
        private readonly IProjectCanteenUoW _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;

        public AuthentificationService(UserManager<User> userManager,
            IProjectCanteenUoW unitOfWork,
            IMapper mapper,
            IJwtService jwtService)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        public async Task<BaseResponseDTO> SignUp<TEntity>(TEntity entity, IValidator<TEntity> validator, string role, IStringLocalizer _stringLocalizer) where TEntity : SignUpBaseDTO
        {
            var result = await validator.ValidateAsync(entity);

            if (!result.IsValid)
            {
                return new SignInResponseDTO
                {
                    Success = false,
                    Errors = result.Errors.Select(x => x.ErrorMessage).ToList()
                };
            }

            var existing_user = await _userManager.FindByEmailAsync(entity.Email);

            if (existing_user != null)
            {
                return new SignInResponseDTO
                {
                    Success = false,
                    Errors = new List<string> { _stringLocalizer["Email is alredy used"] }
                };
            }

            var new_user = _mapper.Map<User>(entity);

            var is_created = await _userManager.CreateAsync(new_user, entity.Password);

            if (!is_created.Succeeded)
            {
                return new SignInResponseDTO
                {
                    Success = false,
                    Errors = is_created.Errors.Select(x => x.Description).ToList()
                };
            }

            try
            {
                switch (role)
                {
                    case Roles.Admin:
                        await CreateAdminAsync(new_user);
                        break;

                    case Roles.SchoolAdmin:
                        await CreateSchoolAdminAsync(new_user, (entity as SignUpSchoolAdminDTO).SchoolId);
                        break;

                    case Roles.Terminal:
                        await CreateTerminalAsync(new_user, (entity as SignUpTerminalDTO).CanteenId);
                        break;

                    case Roles.CanteenWorker:
                        await CreateCanteenWorkerAsync(new_user, (entity as SignUpCanteenWorkerDTO).CanteenId);
                        break;

                    case Roles.ClassTeacher:
                        await CreateClassTeacherAsync(new_user, (entity as SignUpClassTeacherDTO).ClassId);
                        break;

                    case Roles.Parent:
                        await CreateParentAsync(new_user, (entity as SignUpParentDTO).ChildrenId);
                        break;

                    case Roles.Student:
                        await CreateStudentAsync(new_user, (entity as SignUpStudentDTO).ClassId);
                        break;

                    default:
                        throw new Exception();
                }
            }
            catch
            {
                await _userManager.DeleteAsync(new_user);

                return new SignInResponseDTO
                {
                    Success = false,
                    Errors = new List<string> { "Server error" }
                };
            }

            var token = await _jwtService.GenerateJwtTokenAsync(new_user);

            return new SignInResponseDTO
            {
                Success = true,
                Token = token
            };
        }

        public async Task CreateAdminAsync(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            roles.Clear();
            await _userManager.AddToRoleAsync(user, Roles.Admin);
        }

        public async Task CreateSchoolAdminAsync(User user, int schoolId)
        {
            var isAdminExist = await _unitOfWork.ClassTeacherRepository
                                     .GetFirstOrDefaultAsync(x => x.User.Id == user.Id) != null;

            if (isAdminExist)
            {
                throw new Exception();
            }

            var roles = await _userManager.GetRolesAsync(user);
            roles.Clear();
            await _userManager.AddToRoleAsync(user, Roles.SchoolAdmin);

            var schoolAdmin = new SchoolAdmin { User = user, School = new School { Id = schoolId } };

            await _unitOfWork.SchoolAdminRepository.AttachAsync(schoolAdmin);
            await _unitOfWork.SchoolAdminRepository.CreateAsync(schoolAdmin);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task CreateTerminalAsync(User user, int schoolId)
        {
            var isTerminalExist = await _unitOfWork.CanteenRepository
                .GetFirstOrDefaultAsync(x => x.Terminal != null && x.Terminal.Id == user.Id) != null;

            if (isTerminalExist)
            {
                throw new Exception();
            }

            var canteen = await _unitOfWork.CanteenRepository
                .GetFirstOrDefaultAsync(x => x.Id == schoolId);

            if (canteen == null || canteen.Terminal != null)
            {
                throw new Exception();
            }

            var roles = await _userManager.GetRolesAsync(user);
            roles.Clear();
            await _userManager.AddToRoleAsync(user, Roles.Terminal);

            canteen.Terminal = user;

            await _unitOfWork.CanteenRepository.AttachAsync(canteen);
            await _unitOfWork.CanteenRepository.UpdateAsync(canteen);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task CreateCanteenWorkerAsync(User user, int canteenId)
        {
            var isWorkerExist = await _unitOfWork.CanteenWorkerRepository
                                     .GetFirstOrDefaultAsync(x => x.User.Id == user.Id) != null ||
                                 await _unitOfWork.CanteenWorkerRepository
                                     .GetFirstOrDefaultAsync(x => x.Canteen.Id == canteenId) != null;

            if (isWorkerExist)
            {
                throw new Exception();
            }

            var roles = await _userManager.GetRolesAsync(user);
            roles.Clear();
            await _userManager.AddToRoleAsync(user, Roles.CanteenWorker);

            var worker = new CanteenWorker { User = user, Canteen = new Canteen { Id = canteenId } };

            await _unitOfWork.CanteenWorkerRepository.AttachAsync(worker);
            await _unitOfWork.CanteenWorkerRepository.CreateAsync(worker);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task CreateClassTeacherAsync(User user, int classId)
        {
            var isTeacherExist = await _unitOfWork.ClassTeacherRepository
                                     .GetFirstOrDefaultAsync(x => x.User.Id == user.Id) != null ||
                                 await _unitOfWork.ClassTeacherRepository
                                     .GetFirstOrDefaultAsync(x => x.ClassId == classId) != null;

            if (isTeacherExist)
            {
                throw new Exception();
            }

            var roles = await _userManager.GetRolesAsync(user);
            roles.Clear();
            await _userManager.AddToRoleAsync(user, Roles.ClassTeacher);

            var teacher = new ClassTeacher { User = user, Class = new Class { Id = classId } };

            await _unitOfWork.ClassTeacherRepository.AttachAsync(teacher);
            await _unitOfWork.ClassTeacherRepository.CreateAsync(teacher);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task CreateParentAsync(User user, IEnumerable<int> childrenId)
        {
            var isParentExist = await _unitOfWork.ParentRepository
                                     .GetFirstOrDefaultAsync(x => x.User.Id == user.Id) != null;

            if (isParentExist)
            {
                throw new Exception();
            }

            var roles = await _userManager.GetRolesAsync(user);
            roles.Clear();
            await _userManager.AddToRoleAsync(user, Roles.Parent);

            var parent = new Parent
            {
                User = user,
                Children = childrenId.Select(x => new Student { Id = x }).ToList()
            };

            await _unitOfWork.ParentRepository.AttachAsync(parent);
            await _unitOfWork.ParentRepository.CreateAsync(parent);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task CreateStudentAsync(User user, int classId)
        {
            var isStudentExist = await _unitOfWork.StudentRepository
                                     .GetFirstOrDefaultAsync(x => x.User.Id == user.Id) != null;

            if (isStudentExist)
            {
                throw new Exception();
            }

            var roles = await _userManager.GetRolesAsync(user);
            roles.Clear();
            await _userManager.AddToRoleAsync(user, Roles.Student);

            var student = new Student
            {
                User = user,
                Class = new Class { Id = classId },
                IsAllowedToUseAccount = false
            };

            await _unitOfWork.StudentRepository.AttachAsync(student);
            await _unitOfWork.StudentRepository.CreateAsync(student);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
