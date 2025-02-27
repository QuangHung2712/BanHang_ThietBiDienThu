﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH_Core.Commons
{
    public partial class Constants
    {
        public class DefaultValue
        {
            public const string DEFAULT_CONTROLLER_ROUTER = "api/[controller]/[action]";
            public const string DEFAULT_PASSWORD = "defaultpassword";
            public const string DEFAULT_BASE_Directory_IMG = @"D:\Du_An\BanHang_ThietBiDienThu\QLBH_Application\public";
            public const string DEFAULT_IMG_AVATAR = @"D:\Du_An\BanHang_ThietBiDienThu\QLBH_Application\public\images\AvatarDefault.jpg";

        }
        public class AppSettingKeys
        {
            public const string DEFAULT_CONNECTION = "DefaultConnection";
        }
        public class JWT
        {
            public static string Permission = "Permission";
        }
        public class ExceptionMessage
        {
            public const string NOT_FOUND = "{0} không tồn tại.";
            public const string ALREADY_EXIST = "{0} đã tồn tại.";
            public const string IN_USE = "{0} vẫn đang được sử dụng.";
            public const string SUCCESS = "{0} thành công.";
            public const string SHOULD_GREATER_TODAY = "{0} Date is late.";
            public const string INVALID = "{0} không hợp lệ.";
            public const string EMAIL_NOT_ACTIVATED = "Email not activated";
        }
    }
}
