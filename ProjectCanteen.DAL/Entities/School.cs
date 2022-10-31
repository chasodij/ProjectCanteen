namespace ProjectCanteen.DAL.Entities
{
    public class School
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public List<Class> Classes { get; set; } = new List<Class>();
        public List<Canteen> Canteens { get; set; } = new List<Canteen>();
    }
}
