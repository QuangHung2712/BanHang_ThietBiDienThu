using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLNhaTro.API.MiddleWare;
using static QLBH_Core.Commons.Enums;

namespace QLBH_Core.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServerController : ControllerBase
    {
        [HttpGet()]
        [Route("ping")]
        public IActionResult Get()
        {
            return Ok("Pong");
        }
    }
}
