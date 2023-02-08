using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectCanteen.BLL;
using ProjectCanteen.BLL.DTOs.Base;
using ProjectCanteen.BLL.DTOs.Student;
using ProjectCanteen.BLL.Services.Interfaces;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IDietaryRestrictionService _dietaryRestrictionService;
        private readonly IParentService _parentService;
        private readonly IValidator<UpdateStudentDTO> _updateStudentValidator;

        private readonly UserManager<User> _userManager;

        public StudentsController(IStudentService studentService,
            IValidator<UpdateStudentDTO> updateStudentValidator,
            IDietaryRestrictionService dietaryRestrictionService,
            IParentService parentService,
            UserManager<User> userManager)
        {
            _studentService = studentService;
            _updateStudentValidator = updateStudentValidator;
            _dietaryRestrictionService = dietaryRestrictionService;
            _parentService = parentService;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll(int page, int pageSize)
        {
            var students = await _studentService.GetStudentsAsync(page, pageSize);
            return Ok(new
            {
                students = students.students,
                totalCount = students.totalCount
            });
        }

        [HttpGet]
        [Route("{id}/dietary-restrictions")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.Parent)]
        public async Task<IActionResult> GetAllRestrictions([FromRoute] int id)
        {
            var user = await _userManager.GetUserAsync(User);

            var parent = await _parentService.GetParentByUserId(user.Id);

            if (!parent.Children.Any(x => x.Id == id))
            {
                return Unauthorized();
            }

            var restrictions = await _dietaryRestrictionService.GetDietaryRestrictionsOfStudentAsync(id);

            return Ok(restrictions);
        }

        [HttpPut]
        [Route("{id}/dietary-restrictions")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.Parent)]
        public async Task<IActionResult> UpdateRestrictions([FromRoute] int id, List<int> restrictionsId)
        {
            var user = await _userManager.GetUserAsync(User);

            var parent = await _parentService.GetParentByUserId(user.Id);

            if (!parent.Children.Any(x => x.Id == id))
            {
                return Unauthorized();
            }

            var entity = new UpdateStudentDietaryRestrictionsDTO
            {
                DietaryRestrictionsId = restrictionsId,
                Id = id
            };

            await _studentService.UpdateStudentDietaryRestrictionsAsync(entity);

            return Ok(new BaseResponseDTO
            {
                Success = true
            });
        }

        [HttpPut]
        [Route("{id}/tag")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.Parent)]
        public async Task<IActionResult> UpdateTag([FromRoute] int id, string tagId)
        {
            var user = await _userManager.GetUserAsync(User);

            var parent = await _parentService.GetParentByUserId(user.Id);

            if (!parent.Children.Any(x => x.Id == id))
            {
                return Unauthorized();
            }

            var entity = new UpdateStudentTagDTO
            {
                TagId = tagId,
                Id = id
            };

            await _studentService.UpdateStudentTagAsync(entity);

            return Ok(new BaseResponseDTO
            {
                Success = true
            });
        }

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> Edit(UpdateStudentDTO updateStudentDTO)
        {
            var result = await _updateStudentValidator.ValidateAsync(updateStudentDTO);

            if (result.IsValid)
            {
                try
                {
                    await _studentService.UpdateStudentAsync(updateStudentDTO);
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
            var isDeleted = await _studentService.DeleteStudentAsync(id);

            if (isDeleted)
            {
                return Ok(new BaseResponseDTO { Success = true });
            }

            return BadRequest(new BaseResponseDTO
            {
                Success = false,
                Errors = new List<string> { "There are no student with such id" }
            });
        }
    }
}
