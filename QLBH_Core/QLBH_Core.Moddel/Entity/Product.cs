using QLBH_Core.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH_Core.Moddel.Entity
{
    public class Product : BaseMasterData
    {
        public decimal Price { get; set; }
        public string WarrantyPeriod { get; set; } // Thời gian bảo thành
        public long ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }
    }
}
