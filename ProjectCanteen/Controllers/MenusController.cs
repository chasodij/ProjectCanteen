using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectCanteen.BLL;
using ProjectCanteen.BLL.DTOs.Base;
using ProjectCanteen.BLL.DTOs.MenuOfTheDay;
using ProjectCanteen.BLL.Services.Interfaces;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenusController : ControllerBase
    {
        private readonly IMenuOfTheDayService _menuOfTheDayService;
        private readonly IValidator<UpdateMenuOfTheDayDTO> _menuOfTheDayValidator;
        private readonly IValidator<CreateMenuOfTheDayDTO> _createMenuOfTheDayValidator;

        private readonly ICanteenWorkerService _canteenWorkerService;
        private readonly IRightsService _rightsService;
        private readonly UserManager<User> _userManager;
        private readonly IParentService _parentService;

        public MenusController(IMenuOfTheDayService menuOfTheDayService,
            IValidator<UpdateMenuOfTheDayDTO> menuOfTheDayValidator,
            IValidator<CreateMenuOfTheDayDTO> createMenuOfTheDayValidator,
            UserManager<User> userManager,
            ICanteenWorkerService canteenWorkerService,
            IRightsService rightsService,
            IParentService parentService)
        {
            _menuOfTheDayService = menuOfTheDayService;
            _menuOfTheDayValidator = menuOfTheDayValidator;
            _createMenuOfTheDayValidator = createMenuOfTheDayValidator;
            _userManager = userManager;
            _canteenWorkerService = canteenWorkerService;
            _rightsService = rightsService;
            _parentService = parentService;
        }

        [HttpGet]
        [Route("")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.CanteenWorker)]
        public async Task<IActionResult> GetAll(int page, int pageSize)
        {
            var user = await _userManager.GetUserAsync(User);

            var worker = await _canteenWorkerService.GetWorkerIdByUserId(user.Id);

            var menus = await _menuOfTheDayService.GetMenusAsync(page, pageSize, worker.workerId);

            return Ok(new
            {
                menus = menus.menus,
                totalCount = menus.totalCount
            });
        }

        [HttpGet]
        [Route("{date}/dishes/")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.CanteenWorker + "," + Roles.Parent)]
        public async Task<IActionResult> GetDishesForStudent([FromRoute] DateTime date, int canteenId, int studentId)
        {
            var user = await _userManager.GetUserAsync(User);

            if (!await _rightsService.HasUserRightsToChangeStudent(user, studentId, canteenId))
            {
                return Unauthorized();
            }

            var dishes = await _menuOfTheDayService.GetMenuDishesForStudent(studentId, date, canteenId);

            return Ok(dishes);
        }

        [HttpPost]
        [Route("")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.CanteenWorker)]
        public async Task<IActionResult> Create([FromBody] CreateMenuOfTheDayDTO createMenuOfTheDayDTO)
        {
            var result = await _createMenuOfTheDayValidator.ValidateAsync(createMenuOfTheDayDTO);

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
                    var creatingResult = await _menuOfTheDayService.CreateMenuAsync(createMenuOfTheDayDTO, worker.workerId);
                    return creatingResult.Success ? Ok(creatingResult) : BadRequest(creatingResult);
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
        public async Task<IActionResult> Edit(UpdateMenuOfTheDayDTO menuOfTheDayDTO)
        {
            var result = await _menuOfTheDayValidator.ValidateAsync(menuOfTheDayDTO);

            if (result.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                var hasRightsToUpdate = await _rightsService.HasUserRightsToChangeMenu(user, menuOfTheDayDTO.Id);
                if (!hasRightsToUpdate)
                {
                    return Unauthorized(new BaseResponseDTO
                    {
                        Success = false,
                        Errors = new List<string> { "You have no rights to update menu in this canteen" }
                    });
                }

                try
                {
                    await _menuOfTheDayService.UpdateMenuAsync(menuOfTheDayDTO);
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

            var hasRightsToUpdate = await _rightsService.HasUserRightsToChangeMenu(user, id);
            if (!hasRightsToUpdate)
            {
                return Unauthorized(new BaseResponseDTO
                {
                    Success = false,
                    Errors = new List<string> { "You have no rights to delete menu in this canteen" }
                });
            }

            var isDeleted = await _menuOfTheDayService.DeleteMenuAsync(id);

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
