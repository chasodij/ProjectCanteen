using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ProjectCanteen.BLL.DTOs.ClassTeacherDTO;
using ProjectCanteen.BLL.Services.Interfaces;
using ProjectCanteen.DAL.Entities;
using ProjectCanteen.DAL.UnitOfWork;

namespace ProjectCanteen.BLL.Services.Implementations
{
    public class ClassTeacherService : IClassTeacherService
    {
        private readonly IProjectCanteenUoW _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public ClassTeacherService(IProjectCanteenUoW unitOfWork, IMapper mapper, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<bool> DeleteTeacherAsync(int id)
        {
            var teacher = await _unitOfWork.ClassTeacherRepository.GetByIdAsync(id);

            if (teacher == null)
            {
                return false;
            }

            await _unitOfWork.ClassTeacherRepository.DeleteAsync(teacher);

            var user = await _userManager.FindByIdAsync(teacher.User.Id);

            if (user != null)
            {
                await _unitOfWork.UserRepository.DeleteAsync(user);
            }

            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<ClassTeacherDTO> GetTeacherByUserId(string id)
        {
            var teacher = await _unitOfWork.ClassTeacherRepository.GetFirstOrDefaultAsync(x => x.User.Id == id);

            return _mapper.Map<ClassTeacherDTO>(teacher);
        }

        public async Task<(IEnumerable<FullClassTeacherDTO> teachers, int totalCount)> GetTeachersAsync(int page, int pageSize, int schoolId)
        {
            var teachers = await _unitOfWork.ClassTeacherRepository.GetRangeAsync(x => x.Class.School.Id == schoolId, page: page, pageSize: pageSize);

            return (_mapper.Map<IEnumerable<FullClassTeacherDTO>>(teachers.entities), teachers.totalCount);
        }

        public async Task UpdateTeacherAsync(UpdateClassTeacherDTO classTeacherDTO)
        {
            var teacherFromDb = await _unitOfWork.ClassTeacherRepository.GetByIdAsync(classTeacherDTO.Id);

            if (teacherFromDb == null)
            {
                throw new Exception();
            }

            var teacher = _mapper.Map<ClassTeacher>(classTeacherDTO);

            await _unitOfWork.ClassTeacherRepository.UpdateAsync(teacher);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
