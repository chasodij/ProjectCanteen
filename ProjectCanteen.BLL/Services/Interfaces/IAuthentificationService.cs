using FluentValidation;
using Microsoft.Extensions.Localization;
using ProjectCanteen.BLL.DTOs.Authentication;
using ProjectCanteen.BLL.DTOs.Base;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.BLL.Services.Interfaces
{
    public interface IAuthentificationService
    {
        Task<BaseResponseDTO> SignUp<TEntity>(TEntity entity, IValidator<TEntity> validator, string role, IStringLocalizer _stringLocalizer) where TEntity : SignUpBaseDTO;
        Task CreateAdminAsync(User user);
        Task CreateSchoolAdminAsync(User user, int schoolId);
        Task CreateTerminalAsync(User user, int canteenId);
        Task CreateCanteenWorkerAsync(User user, int canteenId);
        Task CreateClassTeacherAsync(User user, int classId);
        Task CreateParentAsync(User user, IEnumerable<int> childrenId);
        Task CreateStudentAsync(User user, int classId);
    }
}
