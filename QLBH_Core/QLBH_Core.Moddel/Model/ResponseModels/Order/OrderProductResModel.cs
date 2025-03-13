using QLBH_Core.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH_Core.Moddel.Model.ResponseModels
{
    public class OrderProductResModel : BaseEntity
    {
        public string ProductName { get; set; }
        public string ProductIMG { get; set; }
        public decimal Price { get; set; }
        public int ProductNumber { get; set; }
    }
}
