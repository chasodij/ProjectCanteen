namespace ProjectCanteen.BLL.DTOs.Canteen
{
    public class CreateCanteenDTO
    {
        public string Name { get; set; }
        public int SchoolId { get; set; }
        public string? TerminalId { get; set; }
        public int? MinHoursToCreateMenu { get; set; }
        public int? MinHoursToOrder { get; set; }
        public decimal? MaxStudentDebt { get; set; }
    }
}
