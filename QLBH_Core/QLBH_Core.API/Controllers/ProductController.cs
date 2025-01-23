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
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPut]
        [Permission(Enums.FeatureCode.Admin)]
        public async Task<ActionResult> CreateEdit([FromForm]CreateEditProductReqModel input,List<IFormFile> img)
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
    }
}
