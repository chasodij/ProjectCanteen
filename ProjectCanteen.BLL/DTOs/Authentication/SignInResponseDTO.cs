using ProjectCanteen.BLL.DTOs.Base;

namespace ProjectCanteen.BLL.DTOs.Authentication
{
    public class SignInResponseDTO : BaseResponseDTO
    {
        public string Token { get; set; }
    }
}
