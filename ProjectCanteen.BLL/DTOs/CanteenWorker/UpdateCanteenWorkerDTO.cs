namespace ProjectCanteen.BLL.DTOs.CanteenWorker
{
    public class UpdateCanteenWorkerDTO
    {
        public int Id { get; set; }
        public int CanteenId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Patronymic { get; set; }
    }
}
