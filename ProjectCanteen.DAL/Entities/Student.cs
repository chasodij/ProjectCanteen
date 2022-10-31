namespace ProjectCanteen.DAL.Entities
{
    public  class Student
    {
        public int Id { get; set; }
        public string TagId { get; set; }
        public User User { get; set; }
        public Class Class { get; set; }
        public bool IsAllowedToUseAccount { get; set; }
        public List<Parent> Parents { get; set; } = new List<Parent>();
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
