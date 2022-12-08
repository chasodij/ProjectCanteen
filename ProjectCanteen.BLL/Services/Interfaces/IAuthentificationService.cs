using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.BLL.Services.Interfaces
{
    public interface IAuthentificationService
    {
        Task CreateAdminAsync(User user);
    }
}
