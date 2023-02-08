namespace ProjectCanteen.BLL.DTOs.Student
{
    public class StudentOrdersDTO
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public List<OrderOfStudentDTO> Orders { get; set; }
    }
}
