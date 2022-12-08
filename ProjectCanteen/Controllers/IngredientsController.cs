using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectCanteen.BLL.DTOs.Base;
using ProjectCanteen.BLL.DTOs.Ingredient;
using ProjectCanteen.BLL.Services.Interfaces;

namespace ProjectCanteen.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.Admin + "," + Roles.CanteenWorker)]
    public class IngredientsController : ControllerBase
    {
        private readonly IIngredientService _ingredientService;
        private readonly IValidator<IngredientDTO> _ingredientValidator;
        private readonly IValidator<CreateIngredientDTO> _createIngredientValidator;

        public IngredientsController(IIngredientService ingredientService,
            IValidator<IngredientDTO> ingredientValidator,
            IValidator<CreateIngredientDTO> createIngredientValidator)
        {
            _ingredientService = ingredientService;
            _ingredientValidator = ingredientValidator;
            _createIngredientValidator = createIngredientValidator;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll()
        {
            var ingredients = await _ingredientService.GetIngredientsAsync();
            return Ok(ingredients);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Create([FromBody] CreateIngredientDTO createIngredientDTO)
        {
            var result = await _createIngredientValidator.ValidateAsync(createIngredientDTO);

            if (result.IsValid)
            {
                try
                {
                    await _ingredientService.CreateIngredientAsync(createIngredientDTO);
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
        public async Task<IActionResult> Edit(IngredientDTO ingredientDTO)
        {
            var result = await _ingredientValidator.ValidateAsync(ingredientDTO);

            if (result.IsValid)
            {
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
        public async Task<IActionResult> Delete(int id)
        {
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
    }
}
