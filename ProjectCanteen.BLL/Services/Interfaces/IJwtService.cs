using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.BLL.Services.Interfaces
{
    public interface IJwtService
    {
        public Task<string> GenerateJwtTokenAsync(User user);
    }
}
