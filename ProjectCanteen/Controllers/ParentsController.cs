using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectCanteen.BLL.DTOs.Base;
using ProjectCanteen.BLL.DTOs.Parent;
using ProjectCanteen.BLL.Services.Interfaces;

namespace ProjectCanteen.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.Admin + "," + Roles.CanteenWorker)]
    public class ParentsController : ControllerBase
    {
        private readonly IParentService _parentService;
        private readonly IValidator<UpdateParentDTO> _updateParentValidator;

        public ParentsController(IParentService parentService,
            IValidator<UpdateParentDTO> updateParentValidator)
        {
            _parentService = parentService;
            _updateParentValidator = updateParentValidator;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll(int page, int pageSize)
        {
            var parents = await _parentService.GetParentsAsync(page, pageSize);
            return Ok(new
            {
                parents = parents.parents,
                totalCount = parents.totalCount
            });
        }

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> Edit(UpdateParentDTO updateParentDTO)
        {
            var result = await _updateParentValidator.ValidateAsync(updateParentDTO);

            if (result.IsValid)
            {
                try
                {
                    await _parentService.UpdateParentAsync(updateParentDTO);
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
            var isDeleted = await _parentService.DeleteParentAsync(id);

            if (isDeleted)
            {
                return Ok(new BaseResponseDTO { Success = true });
            }

            return BadRequest(new BaseResponseDTO
            {
                Success = false,
                Errors = new List<string> { "There are no parent with such id" }
            });
        }
    }
}
