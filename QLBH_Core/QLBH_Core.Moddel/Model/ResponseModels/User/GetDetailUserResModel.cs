using QLBH_Core.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static QLBH_Core.Commons.Enums;

namespace QLBH_Core.Moddel.Model.ResponseModels
{
    public class GetDetailUserResModel : BaseEntity
    {
        public string Name { get; set; }
        public string PathAvatar { get; set; }
        public string Email { get; set; }
    }
}
