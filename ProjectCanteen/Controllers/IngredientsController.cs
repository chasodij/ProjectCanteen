using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectCanteen.BLL;
using ProjectCanteen.BLL.DTOs.Base;
using ProjectCanteen.BLL.DTOs.DietaryRestriction;
using ProjectCanteen.BLL.DTOs.Ingredient;
using ProjectCanteen.BLL.Services.Interfaces;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IngredientsController : ControllerBase
    {
        private readonly IIngredientService _ingredientService;
        private readonly IValidator<UpdateIngredientDTO> _ingredientValidator;
        private readonly IValidator<CreateIngredientDTO> _createIngredientValidator;

        private readonly IDietaryRestrictionService _dietaryRestrictionService;
        private readonly IValidator<DietaryRestrictionDTO> _dietaryRestrictionValidator;
        private readonly IValidator<CreateDietaryRestrictionDTO> _createDietaryRestrictionValidator;

        private readonly ICanteenWorkerService _canteenWorkerService;
        private readonly IRightsService _rightsService;
        private readonly UserManager<User> _userManager;

        public IngredientsController(IIngredientService ingredientService,
            IValidator<UpdateIngredientDTO> ingredientValidator,
            IValidator<CreateIngredientDTO> createIngredientValidator,
            IDietaryRestrictionService dietaryRestrictionService,
            IValidator<DietaryRestrictionDTO> dietaryRestrictionValidator,
            IValidator<CreateDietaryRestrictionDTO> createDietaryRestrictionValidator,
            UserManager<User> userManager,
            ICanteenWorkerService canteenWorkerService,
            IRightsService rightsService)
        {
            _ingredientService = ingredientService;
            _ingredientValidator = ingredientValidator;
            _createIngredientValidator = createIngredientValidator;
            _dietaryRestrictionService = dietaryRestrictionService;
            _dietaryRestrictionValidator = dietaryRestrictionValidator;
            _createDietaryRestrictionValidator = createDietaryRestrictionValidator;
            _userManager = userManager;
            _canteenWorkerService = canteenWorkerService;
            _rightsService = rightsService;
        }

        [HttpGet]
        [Route("")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.Admin + "," + Roles.CanteenWorker)]
        public async Task<IActionResult> GetAll(int page, int pageSize)
        {
            var user = await _userManager.GetUserAsync(User);

            (bool isWorkerExist, int workerId) worker = (false, 0);

            if (await _userManager.IsInRoleAsync(user, Roles.CanteenWorker))
            {
                worker = await _canteenWorkerService.GetWorkerIdByUserId(user.Id);
                if (!worker.isWorkerExist)
                {
                    return Unauthorized(
                        new BaseResponseDTO
                        {
                            Success = false
                        });
                }
            }

            var ingredients = await _ingredientService.GetIngredientsAsync(page, pageSize,
                worker.isWorkerExist ? worker.workerId : null);
            return Ok(new
            {
                ingredients = ingredients.ingredients,
                totalCount = ingredients.totalCount
            });
        }

        [HttpPost]
        [Route("")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.Admin + "," + Roles.CanteenWorker)]
        public async Task<IActionResult> Create([FromBody] CreateIngredientDTO createIngredientDTO)
        {
            var result = await _createIngredientValidator.ValidateAsync(createIngredientDTO);

            if (result.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                (bool isWorkerExist, int workerId) worker = (false, 0);

                if (await _userManager.IsInRoleAsync(user, Roles.CanteenWorker))
                {
                    worker = await _canteenWorkerService.GetWorkerIdByUserId(user.Id);
                    if (!worker.isWorkerExist)
                    {
                        return Unauthorized(
                            new BaseResponseDTO
                            {
                                Success = false
                            });
                    }
                }

                try
                {
                    await _ingredientService.CreateIngredientAsync(createIngredientDTO,
                        worker.isWorkerExist ? worker.workerId : null);
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.Admin + "," + Roles.CanteenWorker)]
        public async Task<IActionResult> Edit(UpdateIngredientDTO ingredientDTO)
        {
            var result = await _ingredientValidator.ValidateAsync(ingredientDTO);

            if (result.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                var hasRightsToUpdate = await _rightsService.HasUserRightsToChangeIngredient(user, ingredientDTO.Id);
                if (!hasRightsToUpdate)
                {
                    return Unauthorized(new BaseResponseDTO
                    {
                        Success = false,
                        Errors = new List<string> { "You have no rights to update ingredient in this canteen" }
                    });
                }

                try
                {
                    await _ingredientService.UpdateIngredientAsync(ingredientDTO);
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.Admin + "," + Roles.CanteenWorker)]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            var hasRightsToUpdate = await _rightsService.HasUserRightsToChangeIngredient(user, id);
            if (!hasRightsToUpdate)
            {
                return Unauthorized(new BaseResponseDTO
                {
                    Success = false,
                    Errors = new List<string> { "You have no rights to delete ingredient in this canteen" }
                });
            }

            var isDeleted = await _ingredientService.DeleteIngredientAsync(id);

            if (isDeleted)
            {
                return Ok(new BaseResponseDTO { Success = true });
            }

            return BadRequest(new BaseResponseDTO
            {
                Success = false,
                Errors = new List<string> { "There are no ingredient with such id" }
            });
        }

        [HttpGet]
        [Route("dietary-restrictions")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.Admin + "," + Roles.CanteenWorker)]
        public async Task<IActionResult> GetAllRestrictions(int page, int pageSize)
        {
            var restrictions = await _dietaryRestrictionService.GetDietaryRestrictionsAsync(page, pageSize);

            return Ok(new
            {
                restrictions = restrictions.restrictions,
                totalCount = restrictions.totalCount
            });
        }

        [HttpPost]
        [Route("dietary-restrictions")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.Admin)]
        public async Task<IActionResult> CreateRestriction([FromBody] CreateDietaryRestrictionDTO createDietaryRestrictionDTO)
        {
            var result = await _createDietaryRestrictionValidator.ValidateAsync(createDietaryRestrictionDTO);

            if (result.IsValid)
            {
                try
                {
                    await _dietaryRestrictionService.CreateDietaryRestrictionAsync(createDietaryRestrictionDTO);
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
        [Route("dietary-restrictions")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.Admin)]
        public async Task<IActionResult> EditRestriction(DietaryRestrictionDTO restrictionDTO)
        {
            var result = await _dietaryRestrictionValidator.ValidateAsync(restrictionDTO);

            if (result.IsValid)
            {
                try
                {
                    await _dietaryRestrictionService.UpdateDietaryRestrictionAsync(restrictionDTO);
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
        [Route("dietary-restrictions")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.Admin)]
        public async Task<IActionResult> DeleteRestriction(int id)
        {
            var isDeleted = await _dietaryRestrictionService.DeleteDietaryRestrictionAsync(id);

            if (isDeleted)
            {
                return Ok(new BaseResponseDTO { Success = true });
            }

            return BadRequest(new BaseResponseDTO
            {
                Success = false,
                Errors = new List<string> { "There are no dietary restriction with such id" }
            });
        }
    }
}
