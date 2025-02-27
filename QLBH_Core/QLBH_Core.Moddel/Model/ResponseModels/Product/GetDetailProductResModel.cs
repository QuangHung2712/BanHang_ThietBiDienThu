using QLBH_Core.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH_Core.Moddel.Model.ResponseModels
{
    public class GetDetailProductResModel : BaseEntity
    {
        public string Name { get; set; }
        public long ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }
        public decimal Price { get; set; }
        public string WarrantyPeriod { get; set; } // Thời gian bảo hành
        public List<InfoProductGetResModel> InfoProduct {  get; set; }
        public List<string> PathImg { get; set; }
    }
}
