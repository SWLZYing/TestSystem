using System;

namespace TestSystem.Persistent.Model
{
    public class Account
    {
        public int f_id { get; set; }

        public string f_account { get; set; }

        public string f_password { get; set; }

        public string f_nickname { get; set; }

        public DateTime f_lastLoginTime { get; set; }

        public DateTime f_createTime { get; set; }

        public DateTime f_updateTime { get; set; }

        public override string ToString()
        {
            return $"{nameof(f_id)}:{f_id},{nameof(f_account)}:{f_account},{nameof(f_password)}:{f_password},{nameof(f_nickname)}:{f_nickname},{nameof(f_lastLoginTime)}:{f_lastLoginTime}";
        }
    }
}
