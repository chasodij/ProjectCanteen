namespace ProjectCanteen.BLL.DTOs.CanteenWorker
{
    public class FullCanteenWorkerDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
        public int CanteenId { get; set; }
        public string CanteenName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Patronymic { get; set; }
    }
}
