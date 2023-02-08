using ProjectCanteen.BLL.DTOs.Base;
using ProjectCanteen.BLL.DTOs.Student;

namespace ProjectCanteen.BLL.DTOs.Class
{
    public class ClassOrdersDTO : BaseResponseDTO
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
        public int ClassTeacherId { get; set; }
        public string ClassTeacherFullName { get; set; }
        public List<StudentOrdersDTO> StudentsOrders { get; set; }
    }
}
