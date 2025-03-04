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
        public string Size { get; set; } // Kích thước
        public int Capacity { get; set; } // Công suất
        public string Manufacturer { get; set; } // Hãng
        public long ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }
    }
}
