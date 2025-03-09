using QLBH_Core.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH_Core.Moddel.Entity
{
    public class Orders : BaseEntity
    {
        public long ProductId { get; set; }
        public Product Product { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; }
        public string SDTCustomer { get; set; }
        public int Gender { get; set; }
        public string? Address { get; set; }
        public string? Note { get; set; }
        public string? Email { get; set; }
    }
}
