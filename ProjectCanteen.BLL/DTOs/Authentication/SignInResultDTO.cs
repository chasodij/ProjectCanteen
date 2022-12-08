using ProjectCanteen.BLL.DTOs.Base;

namespace ProjectCanteen.BLL.DTOs.Authentication
{
    public class SignInResultDTO : BaseResponseDTO
    {
        public string Token { get; set; }
    }
}
