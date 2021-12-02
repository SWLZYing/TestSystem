using System;

namespace TestSystem.Webapi.Models.Tables
{
    public class T_Account
    {
        public int f_id { get; set; }

        public string f_account { get; set; }

        public string f_password { get; set; }

        public string f_nickname { get; set; }

        public DateTime f_lastLoginTime { get; set; }

        public DateTime f_createTime { get; set; }

        public DateTime f_updateTime { get; set; }
    }
}