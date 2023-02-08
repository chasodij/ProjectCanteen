using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectCanteen.BLL.DTOs.Class;
using ProjectCanteen.BLL.Services.Interfaces;
using ProjectCanteen.DAL.Entities;
using ProjectCanteen.DAL.UnitOfWork;

namespace ProjectCanteen.BLL.Services.Implementations
{
    public class ClassService : IClassService
    {
        private readonly IProjectCanteenUoW _unitOfWork;
        private readonly IMapper _mapper;

        public ClassService(IProjectCanteenUoW unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateClassAsync(CreateClassDTO createClassDTO)
        {
            var cur_class = _mapper.Map<Class>(createClassDTO);

            await _unitOfWork.ClassRepository.AttachAsync(cur_class);
            await _unitOfWork.ClassRepository.CreateAsync(cur_class);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> DeleteClassAsync(int id)
        {
            var cur_class = await _unitOfWork.ClassRepository.GetFirstOrDefaultAsync(x => x.Id == id);

            if (cur_class != null)
            {
                await _unitOfWork.ClassRepository.DeleteAsync(cur_class);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<ClassOrdersDTO> GetClassOrders(int id, DateTime firstDate, DateTime lastDate)
        {
            var cur_class = await _unitOfWork.ClassRepository.GetFirstOrDefaultAsync(x => x.Id == id,
                include: func => func.Include(x => x.Students)
                                        .ThenInclude(x => x.Orders)
                                        .ThenInclude(x => x.OrderItems)
                                     .Include(x => x.Students)
                                        .ThenInclude(x => x.Orders)
                                        .ThenInclude(x => x.Student)
                                        .ThenInclude(x => x.User)
                                     .Include(x => x.ClassTeacher).ThenInclude(x => x.User));

            if (cur_class == null)
            {
                throw new Exception();
            }

            cur_class.Students.Select(x => x.Orders = x.Orders.Where(order => order.OrderTime.Date >= firstDate.Date &&
                                                                              order.OrderTime.Date <= lastDate.Date).ToList());

            return _mapper.Map<ClassOrdersDTO>(cur_class);
        }

        public async Task<Class> GetClassByTeacherId(int id)
        {
            var cur_class = await _unitOfWork.ClassRepository.GetFirstOrDefaultAsync(x => x.ClassTeacher.Id == id);

            return cur_class;
        }

        public async Task UpdateClassAsync(UpdateClassDTO classDTO)
        {
            var cur_class = _mapper.Map<Class>(classDTO);

            await _unitOfWork.ClassRepository.AttachAsync(cur_class);

            await _unitOfWork.ClassRepository.UpdateAsync(cur_class);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<(IEnumerable<FullClassDTO> classes, int totalCount)> GetClassesAsync(int page, int pageSize, int schoolId)
        {
            var classes = await _unitOfWork.ClassRepository.GetRangeAsync(x => x.School.Id == schoolId, page: page, pageSize: pageSize,
                    include: func => func.Include(x => x.ClassTeacher).Include(x => x.School));

            return (_mapper.Map<IEnumerable<FullClassDTO>>(classes.entities), classes.totalCount);
        }
    }
}
