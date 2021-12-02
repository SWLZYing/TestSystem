using System;

namespace TestSystem.Webapi.Models.Tables
{
    public class T_AccountLevel
    {
        public int f_id { get; set; }

        public int f_accountId { get; set; }

        public int f_level { get; set; }

        public DateTime f_createTime { get; set; }

        public DateTime f_updateTime { get; set; }
    }
}