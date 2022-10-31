namespace ProjectCanteen.DAL.Entities
{
    public class CanteenWorker
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Canteen Canteen { get; set; }
    }
}
