using QLBH_Core.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH_Core.Moddel.Model.RequestModels
{
    public class ChangePasswordReqModel : BaseEntity
    {
        public string PasswordOld { get; set; }
        public string PasswordNew { get; set; }
    }
}
