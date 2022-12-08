using ProjectCanteen.DAL.Entities;
using ProjectCanteen.DAL.Repositories.Implementations.Dish;
using ProjectCanteen.DAL.Repositories.Implementations.Ingredient;
using ProjectCanteen.DAL.Repositories.Implementations.MenuOfTheDay;
using ProjectCanteen.DAL.Repositories.Implementations.NewFolder;
using ProjectCanteen.DAL.Repositories.Interfaces.Base;
using ProjectCanteen.DAL.Repositories.Interfaces.Dish;
using ProjectCanteen.DAL.Repositories.Interfaces.Ingredient;
using ProjectCanteen.DAL.Repositories.Interfaces.MenuOfTheDay;

namespace ProjectCanteen.DAL.UnitOfWork
{
    public class ProjectCanteenUoW : IProjectCanteenUoW
    {
        private readonly ProjectCanteenDBContext _dBContext;
        private IBaseRepository<Canteen> _canteenRepository;
        private IBaseRepository<CanteenWorker> _canteenWorkerRepository;
        private IBaseRepository<ClassTeacher> _classTeacherRepository;
        private IBaseRepository<DietaryRestriction> _dietaryRestrictionRepository;
        private IDishRepository _dishRepository;
        private IIngredientRepository _ingredientRepository;
        private IMenuOfTheDayRepository _menuOfTheDayRepository;
        private IBaseRepository<MenuSection> _menuSectionRepository;
        private IBaseRepository<Order> _orderRepository;
        private IBaseRepository<Parent> _parentRepository;
        private IBaseRepository<School> _schoolRepository;
        private IBaseRepository<SchoolAdmin> _schoolAdminRepository;
        private IBaseRepository<Student> _studentRepository;
        private IBaseRepository<User> _userRepository;

        public IBaseRepository<Canteen> CanteenRepository =>
            getRepository<BaseRepository<Canteen>, Canteen>(ref _canteenRepository);

        public IBaseRepository<CanteenWorker> CanteenWorkerRepository =>
            getRepository<BaseRepository<CanteenWorker>, CanteenWorker>(ref _canteenWorkerRepository);

        public IBaseRepository<ClassTeacher> ClassTeacherRepository =>
            getRepository<BaseRepository<ClassTeacher>, ClassTeacher>(ref _classTeacherRepository);

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

        public IBaseRepository<Order> OrderRepository =>
            getRepository<BaseRepository<Order>, Order>(ref _orderRepository);

        public IBaseRepository<Parent> ParentRepository =>
            getRepository<BaseRepository<Parent>, Parent>(ref _parentRepository);

        public IBaseRepository<School> SchoolRepository =>
            getRepository<BaseRepository<School>, School>(ref _schoolRepository);

        public IBaseRepository<SchoolAdmin> SchoolAdminRepository =>
            getRepository<BaseRepository<SchoolAdmin>, SchoolAdmin>(ref _schoolAdminRepository);

        public IBaseRepository<Student> StudentRepository =>
            getRepository<BaseRepository<Student>, Student>(ref _studentRepository);

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
    }
}
