﻿using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectCanteen.BLL;
using ProjectCanteen.BLL.DTOs.Base;
using ProjectCanteen.BLL.DTOs.ClassTeacherDTO;
using ProjectCanteen.BLL.Services.Interfaces;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClassTeachersController : ControllerBase
    {
        private readonly IClassTeacherService _classTeacherService;
        private readonly IValidator<UpdateClassTeacherDTO> _updateClassTeacherValidator;

        private readonly UserManager<User> _userManager;
        private readonly ISchoolAdminService _schoolAdminService;

        public ClassTeachersController(IClassTeacherService classTeacherService,
            IValidator<UpdateClassTeacherDTO> updateClassTeacherValidator,
            UserManager<User> userManager,
            ISchoolAdminService schoolAdminService)
        {
            _classTeacherService = classTeacherService;
            _updateClassTeacherValidator = updateClassTeacherValidator;
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

            var teachers = await _classTeacherService.GetTeachersAsync(page, pageSize, admin.School.Id);
            return Ok(new
            {
                teachers = teachers.teachers,
                totalCount = teachers.totalCount
            });
        }

        [HttpPut]
        [Route("")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.SchoolAdmin)]
        public async Task<IActionResult> Edit(UpdateClassTeacherDTO updateClassTeacherDTO)
        {
            var result = await _updateClassTeacherValidator.ValidateAsync(updateClassTeacherDTO);

            if (result.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                var admin = await _schoolAdminService.GetSchoolAdminByUserId(user.Id);

                if (!admin.School.Classes.Any(x => x.Id == updateClassTeacherDTO.ClassId))
                {
                    return Unauthorized();
                }

                try
                {
                    await _classTeacherService.UpdateTeacherAsync(updateClassTeacherDTO);
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

            if (!admin.School.Classes.Any(x => x.ClassTeacher.Id == id))
            {
                return Unauthorized();
            }

            var isDeleted = await _classTeacherService.DeleteTeacherAsync(id);

            if (isDeleted)
            {
                return Ok(new BaseResponseDTO { Success = true });
            }

            return BadRequest(new BaseResponseDTO
            {
                Success = false,
                Errors = new List<string> { "There are no teacher with such id" }
            });
        }
    }
}
