using System.Data.SqlClient;
using Dapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestSystem.Persistent.Repository;
using TestSystem.Persistent.Repository.Interface;

namespace TestSystem.Tests.Repository
{
    [TestClass]
    public class SignRepositoryTests
    {
        const string connectionString = @"Data Source=localhost;Integrated Security=True;database=TestSystem;TrustServerCertificate=True";

        private ISignRepository repo;

        [TestInitialize]
        public void Init()
        {
            using (var cn = new SqlConnection(connectionString))
            {
                // 清空表確保每次單元測試都是獨立的
                string sqlStr = @"TRUNCATE TABLE t_loginRecord
                                  TRUNCATE TABLE t_account
                                  TRUNCATE TABLE t_accountLevel

                                  INSERT INTO t_account (f_account, f_password, f_nickname, f_lastLoginTime, f_createTime, f_updateTime)
                                  VALUES ('USER001', 'PWD001', 'User001', GETDATE(), GETDATE(), GETDATE())

                                  INSERT INTO t_accountLevel (f_accountId, f_level, f_createTime, f_updateTime)
                                  VALUES (1, 1, GETDATE(), GETDATE())";
                cn.Execute(sqlStr);
            }

            repo = new SignRepository(connectionString);
        }

        [TestMethod]
        public void sign_in_success()
        {
            var result = repo.SignIn("USER001", "PWD001");

            Assert.IsTrue(result.isSuccess);
        }

        [TestMethod]
        public void sign_in_failed()
        {
            var result = repo.SignIn("USER001", "PWD00");

            Assert.IsFalse(result.isSuccess);
        }

        [TestMethod]
        public void sign_out()
        {
            var result = repo.SignOut();

            Assert.IsTrue(result.isSuccess);
        }
    }
}
