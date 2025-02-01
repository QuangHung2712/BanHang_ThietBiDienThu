using Microsoft.AspNetCore.Http;
using QLBH_Core.Moddel.Model.RequestModels;
using QLBH_Core.Moddel.Model.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH_Core.Service.Users
{
    public interface IUserService
    {
        GetInfoUserResModel GetInfo(long UserId);
        GetDetailUserResModel GetDetail(long UserId);
        Task Edit(CreateEditUserReqModel input, IFormFile avatar);
        Task Create(CreateEditUserReqModel input);
        Task Delete(long Id);
    }
}
