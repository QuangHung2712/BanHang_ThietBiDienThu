using QLBH_Core.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH_Core.Moddel.Entity
{
    public class OrderProduct : BaseEntity
    {
        public long OrderId { get; set; }
        public Orders Order { get; set; }
        public long ProductId { get; set; }
        public Product Product { get; set; }
        public int ProductQuantity { get; set; } // số lượng sản phẩm
    }
}
