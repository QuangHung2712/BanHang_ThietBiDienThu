using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static QLBH_Core.Commons.Constants;

namespace QLBH_Core.Commons.CustomException
{
    public class AlreadyExistsException : Exception
    {
        public string Name { get; private set; }

        public AlreadyExistsException(string name)
        {
            Name = name;
        }
        public override string Message => string.Format(ExceptionMessage.ALREADY_EXIST, Name);

    }
}
