using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH_Core.Moddel.Model.ResponseModels
{
    public class GetAllProductByTypeResModel
    {
        public string ProductName { get; set; }
        public List<ResultFindProductResModel> Products { get; set; }
    }
}
