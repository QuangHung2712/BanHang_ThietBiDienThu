using Microsoft.AspNetCore.Http;
using QLBH_Core.Moddel.Model.RequestModels;
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
    }
}
