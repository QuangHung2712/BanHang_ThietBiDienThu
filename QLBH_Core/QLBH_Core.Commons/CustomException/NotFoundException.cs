using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static QLBH_Core.Commons.Constants;

namespace QLBH_Core.Commons.CustomException
{
    public class NotFoundException : Exception
    {
        public string Name { get; private set; }
        public NotFoundException(string name)
        {
            Name = name;
        }
        public override string Message => string.Format(ExceptionMessage.NOT_FOUND, Name);
    }
}
