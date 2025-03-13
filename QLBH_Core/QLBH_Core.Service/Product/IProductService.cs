using Microsoft.AspNetCore.Http;
using QLBH_Core.Moddel.Model.RequestModels;
using QLBH_Core.Moddel.Model.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH_Core.Service.ProductS
{
    public interface IProductService
    {
        Task CreateEdit(CreateEditProductReqModel data, List<IFormFile> img);
        Task Delete(long Id);
        GetDetailProductResModel GetDetail(string? nameSlug, long? Id);
        List<GetAllProductResModel> GetAll();
        List<ResultFindProductResModel> FindProduct(string? name);
        GetPriceProductResModel GetPriceByProductName(string? name);
        List<ResultFindProductResModel> GetProductByType(long productType, string nameSlug);
        List<GetAllProductByTypeResModel> GetAllProductByType();
        List<GetAllProductByIdResModel> GetAllProductByID(List<GetAllProductByIdReqModel> Data);
    }
}
