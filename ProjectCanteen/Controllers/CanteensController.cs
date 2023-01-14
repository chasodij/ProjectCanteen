using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectCanteen.BLL;
using ProjectCanteen.BLL.DTOs.Base;
using ProjectCanteen.BLL.DTOs.Canteen;
using ProjectCanteen.BLL.Services.Interfaces;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CanteensController : ControllerBase
    {
        private readonly ICanteenService _canteenService;
        private readonly IValidator<UpdateCanteenDTO> _canteenValidator;
        private readonly IValidator<CreateCanteenDTO> _createCanteenValidator;

        private readonly UserManager<User> _userManager;
        private readonly ISchoolAdminService _schoolAdminService;

        public CanteensController(ICanteenService canteenService,
            IValidator<UpdateCanteenDTO> canteenValidator,
            IValidator<CreateCanteenDTO> createCanteenValidator,
            UserManager<User> userManager,
            ISchoolAdminService schoolAdminService)
        {
            _canteenService = canteenService;
            _canteenValidator = canteenValidator;
            _createCanteenValidator = createCanteenValidator;
            _userManager = userManager;
            _schoolAdminService = schoolAdminService;
        }

        [HttpGet]
        [Route("")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.SchoolAdmin)]
        public async Task<IActionResult> GetAll(int page, int pageSize)
        {
            var user = await _userManager.GetUserAsync(User);

            var admin = await _schoolAdminService.GetSchoolAdminByUserId(user.Id);

            var canteens = await _canteenService.GetCanteensAsync(page, pageSize, admin.School.Id);
            return Ok(new
            {
                canteens = canteens.canteens,
                totalCount = canteens.totalCount
            });
        }

        [HttpPost]
        [Route("")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.SchoolAdmin)]
        public async Task<IActionResult> Create([FromBody] CreateCanteenDTO createCanteenDTO)
        {
            var result = await _createCanteenValidator.ValidateAsync(createCanteenDTO);

            if (result.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                var admin = await _schoolAdminService.GetSchoolAdminByUserId(user.Id);

                if (admin.School.Id != createCanteenDTO.SchoolId)
                {
                    Unauthorized();
                }

                try
                {
                    await _canteenService.CreateCanteenAsync(createCanteenDTO);
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.SchoolAdmin)]
        public async Task<IActionResult> Edit(UpdateCanteenDTO canteenDTO)
        {
            var result = await _canteenValidator.ValidateAsync(canteenDTO);

            if (result.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                var admin = await _schoolAdminService.GetSchoolAdminByUserId(user.Id);

                if (admin.School.Id != canteenDTO.SchoolId)
                {
                    Unauthorized();
                }

                try
                {
                    await _canteenService.UpdateCanteenAsync(canteenDTO);
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
        [Route("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.SchoolAdmin)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var user = await _userManager.GetUserAsync(User);

            var admin = await _schoolAdminService.GetSchoolAdminByUserId(user.Id);

            if (admin.School.Canteens.Any(x => x.Id == id))
            {
                Unauthorized();
            }

            var isDeleted = await _canteenService.DeleteCanteenAsync(id);

            if (isDeleted)
            {
                return Ok(new BaseResponseDTO { Success = true });
            }

            return BadRequest(new BaseResponseDTO
            {
                Success = false,
                Errors = new List<string> { "There are no canteen with such id" }
            });
        }
    }
}
