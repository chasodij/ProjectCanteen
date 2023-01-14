using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectCanteen.BLL.DTOs.Base;
using ProjectCanteen.BLL.DTOs.School;
using ProjectCanteen.BLL.Services.Interfaces;

namespace ProjectCanteen.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.Admin + "," + Roles.CanteenWorker)]
    public class SchoolsController : ControllerBase
    {
        private readonly ISchoolService _schoolService;
        private readonly IValidator<SchoolDTO> _schoolValidator;
        private readonly IValidator<CreateSchoolDTO> _createSchoolValidator;

        public SchoolsController(ISchoolService schoolService,
            IValidator<SchoolDTO> schoolValidator,
            IValidator<CreateSchoolDTO> createSchoolValidator)
        {
            _schoolService = schoolService;
            _schoolValidator = schoolValidator;
            _createSchoolValidator = createSchoolValidator;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll()
        {
            var schools = await _schoolService.GetSchoolsAsync();

            return Ok(schools);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Create([FromBody] CreateSchoolDTO createSchoolDTO)
        {
            var result = await _createSchoolValidator.ValidateAsync(createSchoolDTO);

            if (result.IsValid)
            {
                try
                {
                    await _schoolService.CreateSchoolAsync(createSchoolDTO);
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
        public async Task<IActionResult> Edit(SchoolDTO schoolDTO)
        {
            var result = await _schoolValidator.ValidateAsync(schoolDTO);

            if (result.IsValid)
            {
                try
                {
                    await _schoolService.UpdateSchoolAsync(schoolDTO);
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
            var isDeleted = await _schoolService.DeleteSchoolAsync(id);

            if (isDeleted)
            {
                return Ok(new BaseResponseDTO { Success = true });
            }

            return BadRequest(new BaseResponseDTO
            {
                Success = false,
                Errors = new List<string> { "There are no school with such id" }
            });
        }
    }
}
