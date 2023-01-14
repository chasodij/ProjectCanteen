using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectCanteen.BLL;
using ProjectCanteen.BLL.DTOs.Base;
using ProjectCanteen.BLL.DTOs.Dish;
using ProjectCanteen.BLL.DTOs.MenuSection;
using ProjectCanteen.BLL.Services.Interfaces;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DishesController : ControllerBase
    {
        private readonly IDishService _dishService;
        private readonly IValidator<UpdateDishDTO> _dishValidator;
        private readonly IValidator<CreateDishDTO> _createDishValidator;

        private readonly IMenuSectionService _menuSectionService;
        private readonly IValidator<MenuSectionDTO> _menuSectionValidator;
        private readonly IValidator<CreateMenuSectionDTO> _createMenuSectionValidator;

        private readonly ICanteenWorkerService _canteenWorkerService;
        private readonly IRightsService _rightsService;
        private readonly UserManager<User> _userManager;

        public DishesController(IDishService dishService,
            IValidator<UpdateDishDTO> dishValidator,
            IValidator<CreateDishDTO> createDishValidator,
            IMenuSectionService menuSectionService,
            IValidator<MenuSectionDTO> menuSectionValidator,
            IValidator<CreateMenuSectionDTO> createMenuSectionValidator,
            UserManager<User> userManager,
            ICanteenWorkerService canteenWorkerService,
            IRightsService rightsService)
        {
            _dishService = dishService;
            _dishValidator = dishValidator;
            _createDishValidator = createDishValidator;
            _menuSectionService = menuSectionService;
            _menuSectionValidator = menuSectionValidator;
            _createMenuSectionValidator = createMenuSectionValidator;
            _userManager = userManager;
            _canteenWorkerService = canteenWorkerService;
            _rightsService = rightsService;
        }

        [HttpGet]
        [Route("")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.CanteenWorker)]
        public async Task<IActionResult> GetAll(int page, int pageSize)
        {
            var user = await _userManager.GetUserAsync(User);

            var worker = await _canteenWorkerService.GetWorkerIdByUserId(user.Id);

            var dishes = await _dishService.GetDishesAsync(page, pageSize, workerId: worker.workerId);

            return Ok(new
            {
                dishes = dishes.dishes,
                totalCount = dishes.totalCount
            });
        }

        [HttpPost]
        [Route("")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.CanteenWorker)]
        public async Task<IActionResult> Create([FromBody] CreateDishDTO createDishDTO)
        {
            var result = await _createDishValidator.ValidateAsync(createDishDTO);

            if (result.IsValid)
            {
                try
                {
                    var user = await _userManager.GetUserAsync(User);

                    foreach (var ingredient in createDishDTO.IngredientsInDish)
                    {
                        var hasRightsToCreate = await _rightsService.HasUserRightsToUseIngredient(user, ingredient.IngredientId);
                        if (!hasRightsToCreate)
                        {
                            return Unauthorized(new BaseResponseDTO
                            {
                                Success = false,
                                Errors = new List<string> { "You have no rights to use this ingredient in dish" }
                            });
                        }
                    }

                    var worker = await _canteenWorkerService.GetWorkerIdByUserId(user.Id);

                    await _dishService.CreateDishAsync(createDishDTO, worker.workerId);
                    return Ok(new BaseResponseDTO { Success = true });
                }
                catch
                {
                    return BadRequest(new BaseResponseDTO
                    {
                        Success = false,
                        Errors = new List<string> { "Server error" }
                    });
                }
            }

            return BadRequest(new BaseResponseDTO
            {
                Success = false,
                Errors = result.Errors.Select(x => x.ErrorMessage).ToList()
            });
        }

        [HttpPut]
        [Route("")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.CanteenWorker)]
        public async Task<IActionResult> Edit(UpdateDishDTO updateDishDTO)
        {
            var result = await _dishValidator.ValidateAsync(updateDishDTO);

            if (result.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                foreach (var ingredient in updateDishDTO.IngredientsInDish)
                {
                    var hasRightsToCreate = await _rightsService.HasUserRightsToUseIngredient(user, ingredient.IngredientId);
                    if (!hasRightsToCreate)
                    {
                        return Unauthorized(new BaseResponseDTO
                        {
                            Success = false,
                            Errors = new List<string> { "You have no rights to use this ingredient in dish" }
                        });
                    }
                }

                var hasRightsToUpdate = await _rightsService.HasUserRightsToChangeDish(user, updateDishDTO.Id);
                if (!hasRightsToUpdate)
                {
                    return Unauthorized(new BaseResponseDTO
                    {
                        Success = false,
                        Errors = new List<string> { "You have no rights to update dish in this canteen" }
                    });
                }

                try
                {
                    await _dishService.UpdateDishAsync(updateDishDTO);
                    return Ok(new BaseResponseDTO { Success = true });
                }
                catch
                {
                    return BadRequest(new BaseResponseDTO
                    {
                        Success = false,
                        Errors = new List<string> { "Server error" }
                    });
                }
            }

            return BadRequest(new BaseResponseDTO
            {
                Success = false,
                Errors = result.Errors.Select(x => x.ErrorMessage).ToList()
            });
        }

        [HttpDelete]
        [Route("")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.CanteenWorker)]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            var hasRightsToDelete = await _rightsService.HasUserRightsToChangeDish(user, id);
            if (!hasRightsToDelete)
            {
                return Unauthorized(new BaseResponseDTO
                {
                    Success = false,
                    Errors = new List<string> { "You have no rights to delete dish in this canteen" }
                });
            }

            var isDeleted = await _dishService.DeleteDishAsync(id);

            if (isDeleted)
            {
                return Ok(new BaseResponseDTO { Success = true });
            }

            return BadRequest(new BaseResponseDTO
            {
                Success = false,
                Errors = new List<string> { "There are no dish with such id" }
            });
        }

        [HttpGet]
        [Route("menu-sections")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.Admin + "," + Roles.CanteenWorker)]
        public async Task<IActionResult> GetAllMenuSections(int page, int pageSize)
        {
            var sections = await _menuSectionService.GetMenuSectionsAsync(page, pageSize);

            return Ok(new
            {
                menuSections = sections.menuSections,
                totalCount = sections.totalCount
            });
        }

        [HttpPost]
        [Route("menu-sections")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.Admin)]
        public async Task<IActionResult> CreateMenuSection([FromBody] CreateMenuSectionDTO createMenuSectionDTO)
        {
            var result = await _createMenuSectionValidator.ValidateAsync(createMenuSectionDTO);

            if (result.IsValid)
            {
                try
                {
                    await _menuSectionService.CreateMenuSectionAsync(createMenuSectionDTO);
                    return Ok(new BaseResponseDTO { Success = true });
                }
                catch
                {
                    return BadRequest(new BaseResponseDTO
                    {
                        Success = false,
                        Errors = new List<string> { "Server error" }
                    });
                }
            }

            return BadRequest(new BaseResponseDTO
            {
                Success = false,
                Errors = result.Errors.Select(x => x.ErrorMessage).ToList()
            });
        }

        [HttpPut]
        [Route("menu-sections")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.Admin)]
        public async Task<IActionResult> EditMenuSections(MenuSectionDTO menuSectionDTO)
        {
            var result = await _menuSectionValidator.ValidateAsync(menuSectionDTO);

            if (result.IsValid)
            {
                try
                {
                    await _menuSectionService.UpdateMenuSectionAsync(menuSectionDTO);
                    return Ok(new BaseResponseDTO { Success = true });
                }
                catch
                {
                    return BadRequest(new BaseResponseDTO
                    {
                        Success = false,
                        Errors = new List<string> { "Server error" }
                    });
                }
            }

            return BadRequest(new BaseResponseDTO
            {
                Success = false,
                Errors = result.Errors.Select(x => x.ErrorMessage).ToList()
            });
        }

        [HttpDelete]
        [Route("menu-sections")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.Admin)]
        public async Task<IActionResult> DeleteMenuSections(int id)
        {
            var isDeleted = await _menuSectionService.DeleteMenuSectionAsync(id);

            if (isDeleted)
            {
                return Ok(new BaseResponseDTO { Success = true });
            }

            return BadRequest(new BaseResponseDTO
            {
                Success = false,
                Errors = new List<string> { "There are no menu section with such id" }
            });
        }
    }
}
