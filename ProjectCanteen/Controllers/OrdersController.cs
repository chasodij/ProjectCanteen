using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectCanteen.BLL;
using ProjectCanteen.BLL.DTOs.Base;
using ProjectCanteen.BLL.DTOs.Order;
using ProjectCanteen.BLL.Services.Interfaces;
using ProjectCanteen.DAL.Entities;
using System.Drawing.Printing;

namespace ProjectCanteen.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IValidator<UpdateOrderDTO> _orderValidator;
        private readonly IValidator<CreateOrderDTO> _createOrderValidator;

        private readonly ICanteenWorkerService _canteenWorkerService;
        private readonly IParentService _parentService;
        private readonly IRightsService _rightsService;
        private readonly UserManager<User> _userManager;

        private readonly IMessageService _messageService;

        public OrdersController(IOrderService orderService,
            IValidator<UpdateOrderDTO> orderValidator,
            IValidator<CreateOrderDTO> createOrderValidator,
            UserManager<User> userManager,
            ICanteenWorkerService canteenWorkerService,
            IRightsService rightsService,
            IParentService parentService,
            IMessageService messageService)
        {
            _orderService = orderService;
            _orderValidator = orderValidator;
            _createOrderValidator = createOrderValidator;
            _userManager = userManager;
            _rightsService = rightsService;
            _canteenWorkerService = canteenWorkerService;
            _parentService = parentService;
            _messageService = messageService;
        }

        [HttpGet]
        [Route("")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.CanteenWorker)]
        public async Task<IActionResult> GetAll(int page, int pageSize)
        {
            var user = await _userManager.GetUserAsync(User);

            var worker = await _canteenWorkerService.GetWorkerIdByUserId(user.Id);

            var orders = await _orderService.GetOrdersAsync(page, pageSize, workerId: worker.workerId);

            return Ok(new
            {
                orders = orders.orders,
                totalCount = orders.totalCount
            });
        }

        [HttpGet]
        [Route("date")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.CanteenWorker)]
        public async Task<IActionResult> GetSummaryOfOrders(DateTime date)
        {
            var user = await _userManager.GetUserAsync(User);

            var worker = await _canteenWorkerService.GetWorkerByUserId(user.Id);

            if (worker == null)
            {
                return Unauthorized();
            }

            var orders = await _orderService.GetOrdersOfTheDayAsync(date, worker);

            return Ok(orders);
        }

        [HttpPost]
        [Route("")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.CanteenWorker + "," + Roles.Parent)]
        public async Task<IActionResult> Create([FromBody] CreateOrderDTO createOrderDTO)
        {
            var result = await _createOrderValidator.ValidateAsync(createOrderDTO);

            if (result.IsValid)
            {
                try
                {
                    var user = await _userManager.GetUserAsync(User);

                    var creatingResult = new BaseResponseDTO();

                    var worker = await _canteenWorkerService.GetWorkerByUserId(user.Id);

                    if (worker != null)
                    {
                        creatingResult = await _orderService.CreateOrderAsync(createOrderDTO, worker);
                    }
                    else
                    {
                        var parent = await _parentService.GetParentByUserId(user.Id);

                        if (parent != null)
                        {
                            creatingResult = await _orderService.CreateOrderAsync(createOrderDTO, parent);
                        }
                    }

                    return Ok(creatingResult);
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

        [HttpPost]
        [Route("request")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.Terminal)]
        public async Task<IActionResult> RequestOrder([FromBody]string tagId)
        {
            var user = await _userManager.GetUserAsync(User);

            var result = await _orderService.RequestOrders(tagId, user);

            if (!result.isSuccess)
            {
                return BadRequest(new BaseResponseDTO
                {
                    Success = false
                });
            }

            _messageService.SendMessage(new
            {
                canteenId = result.canteenId,
                ordersId = result.ordersId
            });

            return Ok(new
            {
                Success = true,
                FullName = result.fullStudentName
            });
        }

        [HttpPost]
        [Route("complete")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.CanteenWorker)]
        public async Task<IActionResult> CompleteOrder(int orderId)
        {
            var user = await _userManager.GetUserAsync(User);

            var worker = await _canteenWorkerService.GetWorkerByUserId(user.Id);

            var success = await _orderService.CompleteOrder(orderId, worker);

            return success ? Ok(success) : BadRequest(success);
        }
    }
}
