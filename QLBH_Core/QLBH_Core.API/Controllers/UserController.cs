using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLBH_Core.Commons;
using QLBH_Core.Moddel.Model.RequestModels;
using QLBH_Core.Service.Users;
using QLNhaTro.API.MiddleWare;

namespace QLBH_Core.API.Controllers
{
    [Route(Constants.DefaultValue.DEFAULT_CONTROLLER_ROUTER)]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        //[Authorize]
        [Permission]
        public IActionResult GetInfo([FromQuery] long UserId)
        {
            var result = _userService.GetInfo(UserId);
            return Ok(result);
        }

        [HttpGet("{UserId}")]
        public IActionResult GetDetail(long UserId)
        {
            var result = _userService.GetDetail(UserId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEditUserReqModel data)
        {
            await _userService.Create(data);
            return Ok();
        }

        //[HttpPut]
        //public async Task<IActionResult> Update([FromForm] CreateEditUserReqModel data, [FromForm] IFormFile avatar)
        //{
        //    await _userService.Edit(data, avatar);
        //    return Ok();
        //}

        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(long userId)
        {
            await _userService.Delete(userId);
            return Ok();
        }
    }
}
