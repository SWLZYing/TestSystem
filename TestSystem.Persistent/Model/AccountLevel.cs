using System;

namespace TestSystem.Persistent.Model
{
    public class AccountLevel
    {
        public int f_id { get; set; }

        public int f_accountId { get; set; }

        public int f_level { get; set; }

        public DateTime f_createTime { get; set; }

        public DateTime f_updateTime { get; set; }

        public override string ToString()
        {
            return $"{nameof(f_id)}:{f_id},{nameof(f_accountId)}:{f_accountId},{nameof(f_level)}:{f_level},{nameof(f_createTime)}:{f_createTime},{nameof(f_updateTime)}:{f_updateTime}";
        }
    }
}
