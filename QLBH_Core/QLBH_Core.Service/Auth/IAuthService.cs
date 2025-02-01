using QLBH_Core.Moddel.Model.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH_Core.Service.Auth
{
    public interface IAuthService
    {
        string Login(LoginReqModel input);
        Task ChangePassword(ChangePasswordReqModel data);
    }
}
