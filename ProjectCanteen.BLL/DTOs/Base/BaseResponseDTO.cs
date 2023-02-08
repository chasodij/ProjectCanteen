namespace ProjectCanteen.BLL.DTOs.Base
{
    public class BaseResponseDTO
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; } = new List<string>();

        public static BaseResponseDTO operator +(BaseResponseDTO t1, BaseResponseDTO t2)
        {
            BaseResponseDTO result = new BaseResponseDTO
            {
                Success = t1.Success && t2.Success,
                Errors = new List<string>()
            };
            result.Errors.AddRange(t1.Errors);
            result.Errors.AddRange(t2.Errors);

            return result;
        }
    }
}
