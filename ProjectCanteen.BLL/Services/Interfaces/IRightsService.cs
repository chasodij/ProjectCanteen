using ProjectCanteen.BLL.DTOs.Authentication;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.BLL.Services.Interfaces
{
    public interface IRightsService
    {
        Task<bool> HasUserRightsToChange(User user, SignUpTerminalDTO terminalDTO);
        Task<bool> HasUserRightsToChange(User user, SignUpCanteenWorkerDTO canteenWorkerDTO);
        Task<bool> HasUserRightsToChange(User user, SignUpClassTeacherDTO classTeacherDTO);
        Task<bool> HasUserRightsToChange(User user, SignUpParentDTO parentDTO);
        Task<bool> HasUserRightsToChange(User user, SignUpStudentDTO studentDTO);
        Task<bool> HasUserRightsToUseIngredient(User user, int ingredientId);
        Task<bool> HasUserRightsToChangeIngredient(User user, int ingredientId);
        Task<bool> HasUserRightsToChangeDish(User user, int dishId);
        Task<bool> HasUserRightsToChangeMenu(User user, int menuId);
        Task<bool> HasUserRightsToChangeStudent(User user, int studentId, int canteenId);
        Task<bool> HasUserRightsToChangeCanteen(User user, int canteenId);
    }
}
