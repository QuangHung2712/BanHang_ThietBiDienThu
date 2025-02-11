using QLBH_Core.Moddel.Model;
using QLBH_Core.Moddel.Model.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH_Core.Service.ProductTypeS
{
    public interface IProductTypeService
    {
        List<BaseModel> GetAll();
        Task CreateEdit(CreateEditProductTypeReqModel data);
        Task Delete(long Id);
        List<BaseModel> GetProductypeByName(string? name);
    }
}
