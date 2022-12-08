using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectCanteen.BLL.DTOs.Base;
using ProjectCanteen.BLL.DTOs.Dish;
using ProjectCanteen.BLL.Services.Interfaces;

namespace ProjectCanteen.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.Admin + "," + Roles.CanteenWorker)]
    public class DishesController : ControllerBase
    {
        private readonly IDishService _dishService;
        private readonly IValidator<DishDTO> _dishValidator;
        private readonly IValidator<CreateDishDTO> _createDishValidator;

        public DishesController(IDishService dishService,
            IValidator<DishDTO> dishValidator,
            IValidator<CreateDishDTO> createDishValidator)
        {
            _dishService = dishService;
            _dishValidator = dishValidator;
            _createDishValidator = createDishValidator;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll()
        {
            var dishes = await _dishService.GetDishesAsync();
            return Ok(dishes);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Create([FromBody] CreateDishDTO createDishDTO)
        {
            var result = await _createDishValidator.ValidateAsync(createDishDTO);

            if (result.IsValid)
            {
                try
                {
                    await _dishService.CreateDishAsync(createDishDTO);
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
        public async Task<IActionResult> Edit(DishDTO dishDTO)
        {
            var result = await _dishValidator.ValidateAsync(dishDTO);

            if (result.IsValid)
            {
                try
                {
                    await _dishService.UpdateDishAsync(dishDTO);
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
    }
}
