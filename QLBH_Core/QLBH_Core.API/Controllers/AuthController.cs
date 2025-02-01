using Microsoft.AspNetCore.Mvc;
using QLBH_Core.Commons;
using QLBH_Core.Moddel.Model.RequestModels;
using QLBH_Core.Service.Auth;
using QLNhaTro.API.MiddleWare;

namespace QLBH_Core.API.Controllers
{
    [Route(Constants.DefaultValue.DEFAULT_CONTROLLER_ROUTER)]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost]
        public IActionResult Login([FromBody] LoginReqModel data)
        {
            var result = _authService.Login(data);
            return Ok(result);
        }
        [HttpPut]
        [Permission]
        public async Task<IActionResult> ChangePassword(ChangePasswordReqModel input)
        {
            await _authService.ChangePassword(input);
            return Ok();
        }
    }
}
