namespace ProjectCanteen.BLL.DTOs.Base
{
    public class BaseResponseDTO
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
    }
}
