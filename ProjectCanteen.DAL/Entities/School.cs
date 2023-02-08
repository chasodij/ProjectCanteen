namespace ProjectCanteen.DAL.Entities
{
    public class School
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Class> Classes { get; set; }
        public List<Canteen> Canteens { get; set; }
    }
}
