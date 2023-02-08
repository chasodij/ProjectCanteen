using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectCanteen.BLL;
using ProjectCanteen.BLL.DTOs.Base;
using ProjectCanteen.BLL.DTOs.Canteen;
using ProjectCanteen.BLL.DTOs.CanteenWorker;
using ProjectCanteen.BLL.Services.Implementations;
using ProjectCanteen.BLL.Services.Interfaces;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CanteenWorkersController : ControllerBase
    {
        private readonly ICanteenWorkerService _canteenWorkerService;
        private readonly IValidator<UpdateCanteenWorkerDTO> _updateCanteenWorkerValidator;

        private readonly UserManager<User> _userManager;
        private readonly ISchoolAdminService _schoolAdminService;

        public CanteenWorkersController(ICanteenWorkerService canteenWorkerService,
            IValidator<UpdateCanteenWorkerDTO> updateCanteenWorkerValidator,
            UserManager<User> userManager,
            ISchoolAdminService schoolAdminService)
        {
            _canteenWorkerService = canteenWorkerService;
            _updateCanteenWorkerValidator = updateCanteenWorkerValidator;
            _userManager = userManager;
            _schoolAdminService = schoolAdminService;
        }

        [HttpGet]
        [Route("")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.SchoolAdmin)]
        public async Task<IActionResult> GetAll(int page, int pageSize, int canteenId)
        {
            var user = await _userManager.GetUserAsync(User);

            var admin = await _schoolAdminService.GetSchoolAdminByUserId(user.Id);
            if (!admin.School.Canteens.Any(x => x.Id == canteenId))
            {
                return Unauthorized();
            }

            var workers = await _canteenWorkerService.GetWorkersAsync(page, pageSize, canteenId);
            return Ok(new
            {
                workers = workers.workers,
                totalCount = workers.totalCount
            });
        }

        [HttpPut]
        [Route("")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.SchoolAdmin)]
        public async Task<IActionResult> Edit(UpdateCanteenWorkerDTO updateCanteenWorkerDTO)
        {
            var result = await _updateCanteenWorkerValidator.ValidateAsync(updateCanteenWorkerDTO);

            if (result.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                var admin = await _schoolAdminService.GetSchoolAdminByUserId(user.Id);

                if (!admin.School.Canteens.Any(x => x.Id == updateCanteenWorkerDTO.CanteenId))
                {
                    return Unauthorized();
                }

                try
                {
                    await _canteenWorkerService.UpdateWorkerAsync(updateCanteenWorkerDTO);
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
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var user = await _userManager.GetUserAsync(User);

            var admin = await _schoolAdminService.GetSchoolAdminByUserId(user.Id);

            if (!admin.School.Canteens.Any(x => x.CanteenWorkers.Any(x => x.Id == id)))
            {
                return Unauthorized();
            }

            var isDeleted = await _canteenWorkerService.DeleteWorkerAsync(id);

            if (isDeleted)
            {
                return Ok(new BaseResponseDTO { Success = true });
            }

            return BadRequest(new BaseResponseDTO
            {
                Success = false,
                Errors = new List<string> { "There are no canteen worker with such id" }
            });
        }
    }
}
