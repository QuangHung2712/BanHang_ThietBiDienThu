﻿using QLBH_Core.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static QLBH_Core.Commons.Enums;

namespace QLBH_Core.Moddel.Entity
{
    public class User : BaseMasterData
    {
        public string PathAvatar { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public FeatureCode Position { get; set; }
    }
}
