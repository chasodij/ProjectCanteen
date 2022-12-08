using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using ProjectCanteen.BLL.DTOs.Authentication;
using ProjectCanteen.BLL.Services.Interfaces;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtService _jwtService;
        private readonly IValidator<SignUpAdminDTO> _signUpAdminValidator;
        private readonly IValidator<SignInDTO> _signInValidator;
        private readonly IStringLocalizer<AuthenticationController> _stringLocalizer;
        private readonly IAuthentificationService _authentificationService;

        public AuthenticationController(UserManager<User> userManager,
            IJwtService jwtService,
            IValidator<SignInDTO> signInValidator,
            IValidator<SignUpAdminDTO> signUpAdminValidator,
            IStringLocalizer<AuthenticationController> stringLocalizer,
            IAuthentificationService authentificationService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _signInValidator = signInValidator;
            _signUpAdminValidator = signUpAdminValidator;
            _stringLocalizer = stringLocalizer;
            _authentificationService = authentificationService;
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
                    return BadRequest(new SignInResultDTO
                    {
                        Success = false,
                        Errors = new List<string> { _stringLocalizer["The account with the specified email address does not exist"] }
                    });
                }

                var isPasswordCorrect = await _userManager.CheckPasswordAsync(existing_user, signInDTO.Password);

                if (!isPasswordCorrect)
                {
                    return BadRequest(new SignInResultDTO
                    {
                        Success = false,
                        Errors = new List<string> { _stringLocalizer["Wrong password"] }
                    });
                }

                var token = await _jwtService.GenerateJwtTokenAsync(existing_user);

                return Ok(new SignInResultDTO
                {
                    Success = true,
                    Token = token
                });
            }
            return BadRequest(new SignInResultDTO
            {
                Success = false,
                Errors = result.Errors.Select(x => x.ErrorMessage).ToList()
            });
        }

        [HttpPost]
        [Route("signup/admin")]
        [AllowAnonymous]
        public async Task<IActionResult> AdminSignup([FromBody] SignUpAdminDTO signUpAdminDTO)
        {
            ValidationResult result = await _signUpAdminValidator.ValidateAsync(signUpAdminDTO);
            if (result.IsValid)
            {
                var existing_user = await _userManager.FindByEmailAsync(signUpAdminDTO.Email);

                if (existing_user != null)
                {
                    return BadRequest(new SignInResultDTO
                    {
                        Success = false,
                        Errors = new List<string> { _stringLocalizer["Email is alredy used"] }
                    });
                }

                var new_user = new User
                {
                    Email = signUpAdminDTO.Email,
                    UserName = signUpAdminDTO.Email,
                    FirstName = signUpAdminDTO.FirstName
                };

                var is_created = await _userManager.CreateAsync(new_user, signUpAdminDTO.Password);

                if (is_created.Succeeded)
                {
                    await _authentificationService.CreateAdminAsync(new_user);

                    var token = await _jwtService.GenerateJwtTokenAsync(new_user);

                    return Ok(new SignInResultDTO
                    {
                        Success = true,
                        Token = token
                    });
                }

                return BadRequest(new SignInResultDTO
                {
                    Success = false,
                    Errors = is_created.Errors.Select(x => x.Description).ToList()
                });
            }
            return BadRequest(new SignInResultDTO
            {
                Success = false,
                Errors = result.Errors.Select(x => x.ErrorMessage).ToList()
            });
        }
    }
}
