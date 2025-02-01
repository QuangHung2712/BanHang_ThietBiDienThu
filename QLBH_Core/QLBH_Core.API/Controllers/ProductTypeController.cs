using Microsoft.AspNetCore.Mvc;
using QLBH_Core.Commons;
using QLBH_Core.Moddel.Model;
using QLBH_Core.Moddel.Model.RequestModels;
using QLBH_Core.Service.ProductTypeS;
using QLNhaTro.API.MiddleWare;


namespace QLBH_Core.API.Controllers
{
    [Route(Constants.DefaultValue.DEFAULT_CONTROLLER_ROUTER)]
    [ApiController]
    public class ProductTypeController : ControllerBase
    {
        private readonly IProductTypeService _productTypeService;

        public ProductTypeController(IProductTypeService productTypeService)
        {
            _productTypeService = productTypeService;
        }

        [HttpGet]
        [Permission(Enums.FeatureCode.Manage, Enums.FeatureCode.Admin)]
        public IActionResult GetAll() 
        {
            var result = _productTypeService.GetAll();
            return Ok(result);
        }

        [HttpPut]
        [Permission(Enums.FeatureCode.Admin)]
        public async Task<ActionResult> CreateEdit([FromBody] CreateEditProductTypeReqModel input)
        {
            await _productTypeService.CreateEdit(input);
            return Ok();
        }

        [HttpDelete("{Id}")]
        [Permission(Enums.FeatureCode.Admin)]
        public async Task<ActionResult> Delete(long Id)
        {
            await _productTypeService.Delete(Id);
            return Ok();
        }
    }
}
