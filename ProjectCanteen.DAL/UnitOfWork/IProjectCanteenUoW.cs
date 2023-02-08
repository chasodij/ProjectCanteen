using ProjectCanteen.DAL.Entities;
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
    public interface IProjectCanteenUoW
    {
        ICanteenRepository CanteenRepository { get; }

        ICanteenWorkerRepository CanteenWorkerRepository { get; }

        IClassRepository ClassRepository { get; }

        IClassTeacherRepository ClassTeacherRepository { get; }

        IBaseRepository<DietaryRestriction> DietaryRestrictionRepository { get; }

        IDishRepository DishRepository { get; }

        IIngredientRepository IngredientRepository { get; }

        IMenuOfTheDayRepository MenuOfTheDayRepository { get; }

        IBaseRepository<MenuSection> MenuSectionRepository { get; }

        IOrderRepository OrderRepository { get; }

        IParentRepository ParentRepository { get; }

        IBaseRepository<School> SchoolRepository { get; }

        ISchoolAdminRepository SchoolAdminRepository { get; }

        IStudentRepository StudentRepository { get; }

        IBaseRepository<User> UserRepository { get; }

        Task SaveChangesAsync();
        void CleanChangesTracker();
        void Detach<TEntity>(TEntity entity);
    }
}
