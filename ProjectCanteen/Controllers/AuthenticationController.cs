using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using ProjectCanteen.BLL;
using ProjectCanteen.BLL.DTOs.Authentication;
using ProjectCanteen.BLL.DTOs.Base;
using ProjectCanteen.BLL.Services.Interfaces;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IStringLocalizer<AuthenticationController> _stringLocalizer;

        private readonly IJwtService _jwtService;
        private readonly IAuthentificationService _authentificationService;
        private readonly IRightsService _rightsService;

        private readonly IValidator<SignInDTO> _signInValidator;
        private readonly IValidator<SignUpAdminDTO> _signUpAdminValidator;
        private readonly IValidator<SignUpSchoolAdminDTO> _signUpSchoolAdminValidator;
        private readonly IValidator<SignUpTerminalDTO> _signUpTerminalValidator;
        private readonly IValidator<SignUpCanteenWorkerDTO> _signUpCanteenWorkerValidator;
        private readonly IValidator<SignUpClassTeacherDTO> _signUpClassTeacherValidator;
        private readonly IValidator<SignUpParentDTO> _signUpParentValidator;
        private readonly IValidator<SignUpStudentDTO> _signUpStudentValidator;

        public AuthenticationController(UserManager<User> userManager,
            IStringLocalizer<AuthenticationController> stringLocalizer,
            IJwtService jwtService,
            IAuthentificationService authentificationService,
            IRightsService rightsService,
            IValidator<SignInDTO> signInValidator,
            IValidator<SignUpAdminDTO> signUpAdminValidator,
            IValidator<SignUpSchoolAdminDTO> signUpSchoolAdminValidator,
            IValidator<SignUpTerminalDTO> signUpTerminalValidator,
            IValidator<SignUpCanteenWorkerDTO> signUpCanteenWorkerValidator,
            IValidator<SignUpClassTeacherDTO> signUpClassTeacherValidator,
            IValidator<SignUpParentDTO> signUpParentValidator,
            IValidator<SignUpStudentDTO> signUpStudentValidator)
        {
            _userManager = userManager;
            _stringLocalizer = stringLocalizer;

            _jwtService = jwtService;
            _authentificationService = authentificationService;
            _rightsService = rightsService;

            _signInValidator = signInValidator;
            _signUpAdminValidator = signUpAdminValidator;
            _signUpSchoolAdminValidator = signUpSchoolAdminValidator;
            _signUpTerminalValidator = signUpTerminalValidator;
            _signUpCanteenWorkerValidator = signUpCanteenWorkerValidator;
            _signUpClassTeacherValidator = signUpClassTeacherValidator;
            _signUpParentValidator = signUpParentValidator;
            _signUpStudentValidator = signUpStudentValidator;
        }

        [HttpPost]
        [Route("signin")]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn([FromBody] SignInDTO signInDTO)
        {
            var result = await _signInValidator.ValidateAsync(signInDTO);
            if (result.IsValid)
            {
                var existing_user = await _userManager.FindByEmailAsync(signInDTO.Email);

                if (existing_user == null)
                {
                    return BadRequest(new SignInResponseDTO
                    {
                        Success = false,
                        Errors = new List<string> { _stringLocalizer["The account with the specified email address does not exist"] }
                    });
                }

                var isPasswordCorrect = await _userManager.CheckPasswordAsync(existing_user, signInDTO.Password);

                if (!isPasswordCorrect)
                {
                    return BadRequest(new SignInResponseDTO
                    {
                        Success = false,
                        Errors = new List<string> { _stringLocalizer["Wrong password"] }
                    });
                }

                var token = await _jwtService.GenerateJwtTokenAsync(existing_user);

                return Ok(new SignInResponseDTO
                {
                    Success = true,
                    Token = token
                });
            }
            return BadRequest(new SignInResponseDTO
            {
                Success = false,
                Errors = result.Errors.Select(x => x.ErrorMessage).ToList()
            });
        }

        [HttpPost]
        [Route("signup/admin")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.Admin)]
        public async Task<IActionResult> AdminSignup([FromBody] SignUpAdminDTO signUpAdminDTO)
        {
            var result = await _authentificationService.SignUp(signUpAdminDTO, _signUpAdminValidator, Roles.Admin, _stringLocalizer);

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        [Route("signup/school-admin")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.Admin)]
        public async Task<IActionResult> SchoolAdminSignup([FromBody] SignUpSchoolAdminDTO signUpSchoolAdminDTO)
        {
            var result = await _authentificationService.SignUp(signUpSchoolAdminDTO, _signUpSchoolAdminValidator, Roles.SchoolAdmin, _stringLocalizer);

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        [Route("signup/terminal")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.Admin + "," + Roles.SchoolAdmin)]
        public async Task<IActionResult> TerminalSignup([FromBody] SignUpTerminalDTO signUpTerminalDTO)
        {
            var user = await _userManager.GetUserAsync(User);

            var hasRightsToCreate = await _rightsService.HasUserRightsToChange(user, signUpTerminalDTO);

            if (hasRightsToCreate)
            {
                var result = await _authentificationService.SignUp(signUpTerminalDTO, _signUpTerminalValidator, Roles.Terminal, _stringLocalizer);

                return result.Success ? Ok(result) : BadRequest(result);
            }

            return Unauthorized(new BaseResponseDTO
            {
                Success = false,
                Errors = new List<string> { "You have no rights to create terminal for this canteen" }
            });
        }

        [HttpPost]
        [Route("signup/canteen-worker")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.Admin + "," + Roles.SchoolAdmin)]
        public async Task<IActionResult> CanteenWorkerSignup([FromBody] SignUpCanteenWorkerDTO signUpCanteenWorkerDTO)
        {
            var user = await _userManager.GetUserAsync(User);

            var hasRightsToCreate = await _rightsService.HasUserRightsToChange(user, signUpCanteenWorkerDTO);

            if (hasRightsToCreate)
            {
                var result = await _authentificationService.SignUp(signUpCanteenWorkerDTO, _signUpCanteenWorkerValidator, Roles.CanteenWorker, _stringLocalizer);

                return result.Success ? Ok(result) : BadRequest(result);
            }

            return Unauthorized(new BaseResponseDTO
            {
                Success = false,
                Errors = new List<string> { "You have no rights to create canteen worker for this canteen" }
            });
        }

        [HttpPost]
        [Route("signup/class-teacher")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.Admin + "," + Roles.SchoolAdmin)]
        public async Task<IActionResult> ClassTeacherSignup([FromBody] SignUpClassTeacherDTO signUpClassTeacherDTO)
        {
            var user = await _userManager.GetUserAsync(User);

            var hasRightsToCreate = await _rightsService.HasUserRightsToChange(user, signUpClassTeacherDTO);

            if (hasRightsToCreate)
            {
                var result = await _authentificationService.SignUp(signUpClassTeacherDTO, _signUpClassTeacherValidator, Roles.ClassTeacher, _stringLocalizer);

                return result.Success ? Ok(result) : BadRequest(result);
            }

            return Unauthorized(new BaseResponseDTO
            {
                Success = false,
                Errors = new List<string> { "You have no rights to create class teacher for this class" }
            });
        }

        [HttpPost]
        [Route("signup/parent")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.Admin + "," + Roles.SchoolAdmin)]
        public async Task<IActionResult> ParentSignup([FromBody] SignUpParentDTO signUpParentDTO)
        {
            var user = await _userManager.GetUserAsync(User);

            var hasRightsToCreate = await _rightsService.HasUserRightsToChange(user, signUpParentDTO);

            if (hasRightsToCreate)
            {
                var result = await _authentificationService.SignUp(signUpParentDTO, _signUpParentValidator, Roles.Parent, _stringLocalizer);

                return result.Success ? Ok(result) : BadRequest(result);
            }

            return Unauthorized(new BaseResponseDTO
            {
                Success = false,
                Errors = new List<string> { "You have no rights to create parent for these children" }
            });
        }

        [HttpPost]
        [Route("signup/student")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.Admin + "," + Roles.SchoolAdmin)]
        public async Task<IActionResult> StudentSignup([FromBody] SignUpStudentDTO signUpStudentDTO)
        {
            var user = await _userManager.GetUserAsync(User);

            var hasRightsToCreate = await _rightsService.HasUserRightsToChange(user, signUpStudentDTO);

            if (hasRightsToCreate)
            {
                var result = await _authentificationService.SignUp(signUpStudentDTO, _signUpStudentValidator, Roles.Student, _stringLocalizer);

                return result.Success ? Ok(result) : BadRequest(result);
            }

            return Unauthorized(new BaseResponseDTO
            {
                Success = false,
                Errors = new List<string> { "You have no rights to create student for this class" }
            });
        }
    }
}
