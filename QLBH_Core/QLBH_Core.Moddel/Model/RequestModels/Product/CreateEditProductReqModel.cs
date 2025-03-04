using QLBH_Core.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH_Core.Moddel.Model.RequestModels
{
    public class CreateEditProductReqModel : BaseModel
    {
        public string Size { get; set; } // Kích thước
        public int Capacity { get; set; } // Công suất
        public string Manufacturer { get; set; } // Hãng
        public decimal Price { get; set; }
        public string WarrantyPeriod { get; set; } // Thời gian bảo thành
        public long ProductType { get; set; }
        public List<InfoProductCreateEditReqModel>? InfoProduct { get; set; }
    }
}
