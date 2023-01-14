using AutoMapper;
using ProjectCanteen.BLL.DTOs.School;
using ProjectCanteen.BLL.Services.Interfaces;
using ProjectCanteen.DAL.Entities;
using ProjectCanteen.DAL.UnitOfWork;

namespace ProjectCanteen.BLL.Services.Implementations
{
    public class SchoolService : ISchoolService
    {
        private readonly IProjectCanteenUoW _unitOfWork;
        private readonly IMapper _mapper;

        public SchoolService(IProjectCanteenUoW unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateSchoolAsync(CreateSchoolDTO createSchoolDTO)
        {
            var school = _mapper.Map<School>(createSchoolDTO);

            await _unitOfWork.SchoolRepository.AttachAsync(school);
            await _unitOfWork.SchoolRepository.CreateAsync(school);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> DeleteSchoolAsync(int id)
        {
            var school = await _unitOfWork.SchoolRepository.GetFirstOrDefaultAsync(x => x.Id == id);

            if (school != null)
            {
                await _unitOfWork.SchoolRepository.DeleteAsync(school);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<SchoolDTO>> GetSchoolsAsync()
        {
            var schools = await _unitOfWork.SchoolRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<SchoolDTO>>(schools);
        }

        public async Task<bool> IsSchoolExistWithIdAsync(int id)
        {
            var school = await _unitOfWork.SchoolRepository.GetFirstOrDefaultAsync(x => x.Id == id);

            return school != null;
        }

        public async Task UpdateSchoolAsync(SchoolDTO schoolDTO)
        {
            var school = _mapper.Map<School>(schoolDTO);

            await _unitOfWork.SchoolRepository.AttachAsync(school);

            await _unitOfWork.SchoolRepository.UpdateAsync(school);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
