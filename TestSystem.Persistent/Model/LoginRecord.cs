using System;

namespace TestSystem.Persistent.Model
{
    public class LoginRecord
    {
        public int f_id { get; set; }

        public int f_accountId { get; set; }

        public DateTime f_loginTime { get; set; }

        public DateTime f_createTime { get; set; }

        public DateTime f_updateTime { get; set; }
    }
}
