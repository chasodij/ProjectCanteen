using ProjectCanteen.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCanteen.BLL.DTOs.Class
{
    public class FullClassDTO
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
        public int ClassTeacherId { get; set; }
        public string ClassTeacherFullName { get; set; }
        public int SchoolId { get; set; }
        public string SchoolName { get; set; }
    }
}
