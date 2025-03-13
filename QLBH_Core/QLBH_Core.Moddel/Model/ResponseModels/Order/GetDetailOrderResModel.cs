using QLBH_Core.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH_Core.Moddel.Model.ResponseModels
{
    public class GetDetailOrderResModel
    {
        public List<OrderProductResModel> Products { get; set; }
        public string CustomerName { get; set; }
        public string SDTCustomer { get; set; }
        public string Gender { get; set; }
        public string? Address { get; set; }
        public string? Note { get; set; }
        public string? Email { get; set; }
        public string OrderDate { get; set; }
    }
}
