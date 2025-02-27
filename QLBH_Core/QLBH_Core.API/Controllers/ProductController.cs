using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLBH_Core.Commons;
using QLBH_Core.Moddel.Model.RequestModels;
using QLBH_Core.Service.ProductS;
using QLNhaTro.API.MiddleWare;


namespace QLBH_Core.API.Controllers
{
    [Route(Constants.DefaultValue.DEFAULT_CONTROLLER_ROUTER)]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPut]
        [Permission(Enums.FeatureCode.Admin)]
        public async Task<ActionResult> CreateEdit([FromForm]CreateEditProductReqModel input, [FromForm]List<IFormFile> img)
        {
            await _productService.CreateEdit(input, img);
            return Ok();
        }

        [HttpDelete("{Id}")]
        [Permission(Enums.FeatureCode.Admin)]
        public async Task<ActionResult> Delete(long Id)
        {
            await _productService.Delete(Id);
            return Ok();
        }
        [HttpGet]
        [Permission(Enums.FeatureCode.Admin)]
        public IActionResult GetAll()
        {
            var result = _productService.GetAll();
            return Ok(result);
        }
        [HttpGet("{Id}")]
        public IActionResult GetDetail(long Id)
        {
            var result = _productService.GetDetail(Id);
            return Ok(result);
        }
        [HttpGet]
        public IActionResult GetPrice([FromQuery] string? name)
        {
            var result = _productService.GetPriceByProductName(name);
            return Ok(result);
        }
        [HttpGet]
        public IActionResult FindProduct([FromQuery] string? name)
        {
            var result = _productService.FindProduct(name);
            return Ok(result);
        }
        [HttpGet]
        public IActionResult GetProductByType([FromQuery] long productType, [FromQuery] long Id)
        {
            var result = _productService.GetProductByType(productType,Id);
            return Ok(result);
        }
    }
}
