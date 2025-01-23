using QLBH_Core.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH_Core.Moddel.Entity
{
    public class InfoProduct : BaseEntity
    {
        public long ProductId { get; set; }
        public Product Product { get; set; }
        public string Name { get; set; }
        public string Describe { get; set; }
    }
}
