using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectCanteen.BLL;
using ProjectCanteen.BLL.DTOs.Base;
using ProjectCanteen.BLL.DTOs.Class;
using ProjectCanteen.BLL.Services.Interfaces;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClassesController : ControllerBase
    {
        private readonly IClassService _classService;
        private readonly IClassTeacherService _classTeacherService;
        private readonly IValidator<UpdateClassDTO> _classValidator;
        private readonly IValidator<CreateClassDTO> _createClassValidator;

        private readonly UserManager<User> _userManager;
        private readonly ISchoolAdminService _schoolAdminService;

        public ClassesController(IClassService classService,
            IClassTeacherService userService,
            IValidator<UpdateClassDTO> classValidator,
            IValidator<CreateClassDTO> createClassValidator,
            UserManager<User> userManager,
            ISchoolAdminService schoolAdminService)
        {
            _classService = classService;
            _classValidator = classValidator;
            _createClassValidator = createClassValidator;
            _userManager = userManager;
            _classTeacherService = userService;
            _schoolAdminService = schoolAdminService;
        }

        [HttpGet]
        [Route("")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.SchoolAdmin)]
        public async Task<IActionResult> GetAll(int page, int pageSize)
        {
            var user = await _userManager.GetUserAsync(User);

            var admin = await _schoolAdminService.GetSchoolAdminByUserId(user.Id);

            var classes = await _classService.GetClassesAsync(page, pageSize, admin.School.Id);

            return Ok(new
            {
                classes = classes.classes,
                totalCount = classes.totalCount
            });
        }

        [HttpPost]
        [Route("")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.SchoolAdmin)]
        public async Task<IActionResult> Create([FromBody] CreateClassDTO createClassDTO)
        {
            var result = await _createClassValidator.ValidateAsync(createClassDTO);

            if (result.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                var admin = await _schoolAdminService.GetSchoolAdminByUserId(user.Id);

                if (admin.School.Id != createClassDTO.SchoolId)
                {
                    return Unauthorized();
                }

                try
                {
                    await _classService.CreateClassAsync(createClassDTO);
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
        public async Task<IActionResult> Edit(UpdateClassDTO classDTO)
        {
            var result = await _classValidator.ValidateAsync(classDTO);

            if (result.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                var admin = await _schoolAdminService.GetSchoolAdminByUserId(user.Id);

                if (admin.School.Id != classDTO.SchoolId)
                {
                    return Unauthorized();
                }

                try
                {
                    await _classService.UpdateClassAsync(classDTO);
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
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            var user = await _userManager.GetUserAsync(User);

            var admin = await _schoolAdminService.GetSchoolAdminByUserId(user.Id);

            if (!admin.School.Classes.Any(x => x.Id == id))
            {
                return Unauthorized();
            }

            var isDeleted = await _classService.DeleteClassAsync(id);

            if (isDeleted)
            {
                return Ok(new BaseResponseDTO { Success = true });
            }

            return BadRequest(new BaseResponseDTO
            {
                Success = false,
                Errors = new List<string> { "There are no class with such id" }
            });
        }

        [HttpGet]
        [Route("my-class")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.ClassTeacher)]
        public async Task<IActionResult> GetMyClassOrders(DateTime firstDate, DateTime lastDate)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);

                var teacher = await _classTeacherService.GetTeacherByUserId(user.Id);

                var cur_class = await _classService.GetClassByTeacherId(teacher.Id);

                var class_orders = await _classService.GetClassOrders(cur_class.Id, firstDate, lastDate);

                class_orders.Success = true;

                return Ok(class_orders);
            }
            catch
            {
                return BadRequest(new BaseResponseDTO
                {
                    Success = false,
                    Errors = new List<string> { "There are no class with such id" }
                });
            }
        }
    }
}
