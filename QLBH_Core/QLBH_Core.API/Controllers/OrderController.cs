using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLBH_Core.Commons;
using QLBH_Core.Moddel.Model.RequestModels;
using QLBH_Core.Service.Order;
using QLNhaTro.API.MiddleWare;


namespace QLBH_Core.API.Controllers
{
    [Route(Constants.DefaultValue.DEFAULT_CONTROLLER_ROUTER)]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateOrderReqModel input)
        {
            await _orderService.Create(input);
            return Ok();
        }
        [HttpGet]
        [Permission(Enums.FeatureCode.Admin)]
        public IActionResult GetAll()
        {
            var result = _orderService.GetAll();
            return Ok(result);
        }
    }
}
