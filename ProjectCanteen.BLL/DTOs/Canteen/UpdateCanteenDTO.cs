﻿namespace ProjectCanteen.BLL.DTOs.Canteen
{
    public class UpdateCanteenDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SchoolId { get; set; }
        public string? TerminalId { get; set; }
        public int? MinHoursToCreateMenu { get; set; }
        public int? MinHoursToOrder { get; set; }
        public decimal? MaxStudentDebt { get; set; }
    }
}
