using ProjectCanteen.DAL.Entities;
using ProjectCanteen.DAL.Repositories.Interfaces.Base;
using ProjectCanteen.DAL.Repositories.Interfaces.Dish;
using ProjectCanteen.DAL.Repositories.Interfaces.Ingredient;
using ProjectCanteen.DAL.Repositories.Interfaces.MenuOfTheDay;

namespace ProjectCanteen.DAL.UnitOfWork
{
    public interface IProjectCanteenUoW
    {
        IBaseRepository<Canteen> CanteenRepository { get; }

        IBaseRepository<CanteenWorker> CanteenWorkerRepository { get; }

        IBaseRepository<ClassTeacher> ClassTeacherRepository { get; }

        IBaseRepository<DietaryRestriction> DietaryRestrictionRepository { get; }

        IDishRepository DishRepository { get; }

        IIngredientRepository IngredientRepository { get; }

        IMenuOfTheDayRepository MenuOfTheDayRepository { get; }

        IBaseRepository<MenuSection> MenuSectionRepository { get; }

        IBaseRepository<Order> OrderRepository { get; }

        IBaseRepository<Parent> ParentRepository { get; }

        IBaseRepository<School> SchoolRepository { get; }

        IBaseRepository<SchoolAdmin> SchoolAdminRepository { get; }

        IBaseRepository<Student> StudentRepository { get; }

        IBaseRepository<User> UserRepository { get; }

        Task SaveChangesAsync();
    }
}
