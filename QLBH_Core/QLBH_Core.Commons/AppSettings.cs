﻿namespace QLBH_Core.Commons
{
    public class AppSettings
    {
        public Jwt Jwt { get; set; }
    }
    public class Jwt
    {

        public string SecretKey { get; set; }
    }
    public class EmailSetting
    {
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string SenderName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool EnableSsl { get; set; }
    }
}
