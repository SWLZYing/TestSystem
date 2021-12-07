using Dapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SqlClient;
using TestSystem.Persistent.Repository;
using TestSystem.Persistent.Repository.Interface;

namespace TestSystem.Tests.Repository
{
    [TestClass]
    public class AccountLevelRepositoryTests
    {
        const string connectionString = @"Data Source=localhost;Integrated Security=True;database=TestSystem;TrustServerCertificate=True";

        private IAccountLevelRepository repo;

        [TestInitialize]
        public void Into()
        {
            using (var cn = new SqlConnection(connectionString))
            {
                // 重置表確保每次單元測試都是獨立的
                string sqlStr = @"TRUNCATE TABLE t_accountLevel

                                  INSERT INTO t_accountLevel (f_accountId, f_level, f_createTime, f_updateTime)
                                  VALUES (1, 1, GETDATE(), GETDATE())";
                cn.Execute(sqlStr);
            }

            repo = new AccountLevelRepository(connectionString);
        }

        [TestMethod]
        public void set_account_level()
        {
            var result = repo.Create(2);

            Assert.IsNull(result.exception);
            Assert.IsNotNull(result.result);
            Console.WriteLine(result.result.ToString());
        }
    }
}
