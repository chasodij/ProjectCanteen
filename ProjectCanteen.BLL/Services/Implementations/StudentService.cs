using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ProjectCanteen.BLL.DTOs.Student;
using ProjectCanteen.BLL.Services.Interfaces;
using ProjectCanteen.DAL.Entities;
using ProjectCanteen.DAL.UnitOfWork;

namespace ProjectCanteen.BLL.Services.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly IProjectCanteenUoW _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public StudentService(IProjectCanteenUoW unitOfWork, IMapper mapper, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            var student = await _unitOfWork.StudentRepository.GetByIdAsync(id);

            if (student == null)
            {
                return false;
            }

            await _unitOfWork.StudentRepository.DeleteAsync(student);

            var user = await _userManager.FindByIdAsync(student.User.Id);

            if (user != null)
            {
                await _unitOfWork.UserRepository.DeleteAsync(user);
            }

            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<(IEnumerable<FullStudentDTO> students, int totalCount)> GetStudentsAsync(int page, int pageSize)
        {
            var students = await _unitOfWork.StudentRepository.GetRangeAsync(page: page, pageSize: pageSize);

            return (_mapper.Map<IEnumerable<FullStudentDTO>>(students.entities), students.totalCount);
        }

        public async Task UpdateStudentAsync(UpdateStudentDTO studentDTO)
        {
            var student = _mapper.Map<Student>(studentDTO);

            await _unitOfWork.StudentRepository.UpdateAsync(student);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateStudentDietaryRestrictionsAsync(UpdateStudentDietaryRestrictionsDTO restrictionsDTO)
        {
            var student = _mapper.Map<Student>(restrictionsDTO);

            await _unitOfWork.StudentRepository.UpdateRestrictionsAsync(student);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateStudentTagAsync(UpdateStudentTagDTO tagDTO)
        {
            var student = _mapper.Map<Student>(tagDTO);

            await _unitOfWork.StudentRepository.UpdateTagAsync(student);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
