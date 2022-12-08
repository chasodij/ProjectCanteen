using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectCanteen.BLL.DTOs.Base;
using ProjectCanteen.BLL.DTOs.MenuOfTheDay;
using ProjectCanteen.BLL.Services.Interfaces;

namespace ProjectCanteen.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.Admin + "," + Roles.CanteenWorker)]
    public class MenusController : ControllerBase
    {
        private readonly IMenuOfTheDayService _menuOfTheDayService;
        private readonly IValidator<MenuOfTheDayDTO> _menuOfTheDayValidator;
        private readonly IValidator<CreateMenuOfTheDayDTO> _createMenuOfTheDayValidator;

        public MenusController(IMenuOfTheDayService menuOfTheDayService,
            IValidator<MenuOfTheDayDTO> menuOfTheDayValidator,
            IValidator<CreateMenuOfTheDayDTO> createMenuOfTheDayValidator)
        {
            _menuOfTheDayService = menuOfTheDayService;
            _menuOfTheDayValidator = menuOfTheDayValidator;
            _createMenuOfTheDayValidator = createMenuOfTheDayValidator;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll()
        {
            var menus = await _menuOfTheDayService.GetMenuOfTheDayAsync();

            return Ok(menus);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Create([FromBody] CreateMenuOfTheDayDTO createMenuOfTheDayDTO)
        {
            var result = await _createMenuOfTheDayValidator.ValidateAsync(createMenuOfTheDayDTO);

            if (result.IsValid)
            {
                try
                {
                    await _menuOfTheDayService.CreateMenuOfTheDayAsync(createMenuOfTheDayDTO);
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
        public async Task<IActionResult> Edit(MenuOfTheDayDTO menuOfTheDayDTO)
        {
            var result = await _menuOfTheDayValidator.ValidateAsync(menuOfTheDayDTO);

            if (result.IsValid)
            {
                try
                {
                    await _menuOfTheDayService.UpdateMenuOfTheDayAsync(menuOfTheDayDTO);
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
            var isDeleted = await _menuOfTheDayService.DeleteMenuOfTheDayAsync(id);

            if (isDeleted)
            {
                return Ok(new BaseResponseDTO { Success = true });
            }

            return BadRequest(new BaseResponseDTO
            {
                Success = false,
                Errors = new List<string> { "There are no menu with such id" }
            });
        }
    }
}
