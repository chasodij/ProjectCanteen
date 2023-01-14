using Microsoft.EntityFrameworkCore;
using ProjectCanteen.DAL.Entities;
using ProjectCanteen.DAL.Repositories.Implementations.Canteen;
using ProjectCanteen.DAL.Repositories.Implementations.CanteenWorker;
using ProjectCanteen.DAL.Repositories.Implementations.Class;
using ProjectCanteen.DAL.Repositories.Implementations.ClassTeacher;
using ProjectCanteen.DAL.Repositories.Implementations.Dish;
using ProjectCanteen.DAL.Repositories.Implementations.Ingredient;
using ProjectCanteen.DAL.Repositories.Implementations.MenuOfTheDay;
using ProjectCanteen.DAL.Repositories.Implementations.NewFolder;
using ProjectCanteen.DAL.Repositories.Implementations.Order;
using ProjectCanteen.DAL.Repositories.Implementations.Parent;
using ProjectCanteen.DAL.Repositories.Implementations.SchoolAdmin;
using ProjectCanteen.DAL.Repositories.Implementations.Student;
using ProjectCanteen.DAL.Repositories.Interfaces.Base;
using ProjectCanteen.DAL.Repositories.Interfaces.Canteen;
using ProjectCanteen.DAL.Repositories.Interfaces.CanteenWorker;
using ProjectCanteen.DAL.Repositories.Interfaces.Class;
using ProjectCanteen.DAL.Repositories.Interfaces.ClassTeacher;
using ProjectCanteen.DAL.Repositories.Interfaces.Dish;
using ProjectCanteen.DAL.Repositories.Interfaces.Ingredient;
using ProjectCanteen.DAL.Repositories.Interfaces.MenuOfTheDay;
using ProjectCanteen.DAL.Repositories.Interfaces.Order;
using ProjectCanteen.DAL.Repositories.Interfaces.Parent;
using ProjectCanteen.DAL.Repositories.Interfaces.SchoolAdmin;
using ProjectCanteen.DAL.Repositories.Interfaces.Student;

namespace ProjectCanteen.DAL.UnitOfWork
{
    public class ProjectCanteenUoW : IProjectCanteenUoW
    {
        private readonly ProjectCanteenDBContext _dBContext;
        private ICanteenRepository _canteenRepository;
        private ICanteenWorkerRepository _canteenWorkerRepository;
        private IClassTeacherRepository _classTeacherRepository;
        private IClassRepository _classRepository;
        private IBaseRepository<DietaryRestriction> _dietaryRestrictionRepository;
        private IDishRepository _dishRepository;
        private IIngredientRepository _ingredientRepository;
        private IMenuOfTheDayRepository _menuOfTheDayRepository;
        private IBaseRepository<MenuSection> _menuSectionRepository;
        private IOrderRepository _orderRepository;
        private IParentRepository _parentRepository;
        private IBaseRepository<School> _schoolRepository;
        private ISchoolAdminRepository _schoolAdminRepository;
        private IStudentRepository _studentRepository;
        private IBaseRepository<User> _userRepository;

        public ICanteenRepository CanteenRepository
        {
            get
            {
                if (_canteenRepository == null)
                {
                    _canteenRepository = new CanteenRepository();
                    _canteenRepository.BindContext(_dBContext);
                }
                return _canteenRepository;
            }
        }

        public ICanteenWorkerRepository CanteenWorkerRepository
        {
            get
            {
                if (_canteenWorkerRepository == null)
                {
                    _canteenWorkerRepository = new CanteenWorkerRepository();
                    _canteenWorkerRepository.BindContext(_dBContext);
                }
                return _canteenWorkerRepository;
            }
        }

        public IClassTeacherRepository ClassTeacherRepository
        {
            get
            {
                if (_classTeacherRepository == null)
                {
                    _classTeacherRepository = new ClassTeacherRepository();
                    _classTeacherRepository.BindContext(_dBContext);
                }
                return _classTeacherRepository;
            }
        }

        public IClassRepository ClassRepository
        {
            get
            {
                if (_classRepository == null)
                {
                    _classRepository = new ClassRepository();
                    _classRepository.BindContext(_dBContext);
                }
                return _classRepository;
            }
        }

        public IBaseRepository<DietaryRestriction> DietaryRestrictionRepository =>
            getRepository<BaseRepository<DietaryRestriction>, DietaryRestriction>(ref _dietaryRestrictionRepository);

        public IDishRepository DishRepository
        {
            get
            {
                if (_dishRepository == null)
                {
                    _dishRepository = new DishRepository();
                    _dishRepository.BindContext(_dBContext);
                }
                return _dishRepository;
            }
        }

        public IIngredientRepository IngredientRepository
        {
            get
            {
                if (_ingredientRepository == null)
                {
                    _ingredientRepository = new IngredientRepository();
                    _ingredientRepository.BindContext(_dBContext);
                }
                return _ingredientRepository;
            }
        }

        public IMenuOfTheDayRepository MenuOfTheDayRepository
        {
            get
            {
                if (_menuOfTheDayRepository == null)
                {
                    _menuOfTheDayRepository = new MenuOfTheDayRepository();
                    _menuOfTheDayRepository.BindContext(_dBContext);
                }
                return _menuOfTheDayRepository;
            }
        }

        public IBaseRepository<MenuSection> MenuSectionRepository =>
            getRepository<BaseRepository<MenuSection>, MenuSection>(ref _menuSectionRepository);

        public IOrderRepository OrderRepository
        {
            get
            {
                if (_orderRepository == null)
                {
                    _orderRepository = new OrderRepository();
                    _orderRepository.BindContext(_dBContext);
                }
                return _orderRepository;
            }
        }

        public IParentRepository ParentRepository
        {
            get
            {
                if (_parentRepository == null)
                {
                    _parentRepository = new ParentRepository();
                    _parentRepository.BindContext(_dBContext);
                }
                return _parentRepository;
            }
        }

        public IBaseRepository<School> SchoolRepository =>
            getRepository<BaseRepository<School>, School>(ref _schoolRepository);

        public ISchoolAdminRepository SchoolAdminRepository
        {
            get
            {
                if (_schoolAdminRepository == null)
                {
                    _schoolAdminRepository = new SchoolAdminRepository();
                    _schoolAdminRepository.BindContext(_dBContext);
                }
                return _schoolAdminRepository;
            }
        }

        public IStudentRepository StudentRepository
        {
            get
            {
                if (_studentRepository == null)
                {
                    _studentRepository = new StudentRepository();
                    _studentRepository.BindContext(_dBContext);
                }
                return _studentRepository;
            }
        }

        public IBaseRepository<User> UserRepository =>
            getRepository<BaseRepository<User>, User>(ref _userRepository);

        public ProjectCanteenUoW(ProjectCanteenDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task SaveChangesAsync()
        {
            await _dBContext.SaveChangesAsync();
        }

        private IBaseRepository<TEnity> getRepository<TRepository, TEnity>(ref IBaseRepository<TEnity> repository)
            where TEnity : class
            where TRepository : IBaseRepository<TEnity>, new()
        {
            if (repository == null)
            {
                repository = new TRepository();
                repository.BindContext(_dBContext);
            }
            return repository;
        }

        public void CleanChangesTracker()
        {
            _dBContext.ChangeTracker.Clear();
        }

        public void Detach<TEntity>(TEntity entity)
        {
            _dBContext.Entry(entity).State = EntityState.Detached;
        }
    }
}
