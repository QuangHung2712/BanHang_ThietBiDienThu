using QLBH_Core.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH_Core.Moddel.Model.ResponseModels.Product
{
    public class GetAllProductResModel : BaseEntity
    {
        public string Name { get; set; }
        public string ProductTypeName { get; set; }
        public decimal Price  { get; set; }
    }
}
