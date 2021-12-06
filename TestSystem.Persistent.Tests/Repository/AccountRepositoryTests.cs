using Dapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SqlClient;
using TestSystem.Persistent.Model;
using TestSystem.Persistent.Repository;
using TestSystem.Persistent.Repository.Interface;

namespace TestSystem.Persistent.Tests.Repository
{
    [TestClass]
    public class AccountRepositoryTests
    {
        const string connectionString = @"Data Source=localhost;Integrated Security=True;database=TestSystem;TrustServerCertificate=True";

        private IAccountRepository repo;

        [TestInitialize]
        public void Into()
        {
            using (var cn = new SqlConnection(connectionString))
            {
                // 重置表確保每次單元測試都是獨立的
                string sqlStr = @"TRUNCATE TABLE t_account
                                  TRUNCATE TABLE t_accountLevel

                                  INSERT INTO t_account (f_account, f_password, f_nickname, f_lastLoginTime, f_createTime, f_updateTime)
                                  VALUES ('USER001', 'PWD001', 'User001', GETDATE(), GETDATE(), GETDATE())

                                  INSERT INTO t_accountLevel (f_accountId, f_level, f_createTime, f_updateTime)
                                  VALUES (1, 1, GETDATE(), GETDATE())";
                cn.Execute(sqlStr);
            }

            repo = new AccountRepository(connectionString);
        }

        [TestMethod]
        public void set_account()
        {
            var result = repo.Create(new Account
            {
                f_account = "USER002",
                f_password = "PWD002",
                f_nickname = "User002",
            });

            Assert.IsNull(result.exception);
            Assert.IsNotNull(result.result);
            Console.WriteLine(result.result.ToString());
        }

        [TestMethod]
        public void set_account_exist()
        {
            var result = repo.Create(new Account
            {
                f_account = "USER001",
                f_password = "PWD001",
                f_nickname = "User001",
            });

            Assert.IsNull(result.result);
            Assert.IsNotNull(result.exception);
            Console.WriteLine(result.exception.Message);
        }

        [TestMethod]
        public void get_account()
        {
            var result = repo.Query(1);

            Assert.IsNull(result.exception);
            Assert.IsNotNull(result.result);
            Console.WriteLine(result.result.ToString());
        }

        [TestMethod]
        public void get_account_not_exist()
        {
            var result = repo.Query(3);

            Assert.IsNull(result.exception);
            Assert.IsNull(result.result);
        }

        [TestMethod]
        public void update_account_password()
        {
            var result = repo.Update(new Account
            {
                f_id = 1,
                f_password = "ChangePWD001",
                f_nickname = "ChangeUser001",
            });

            Assert.IsNull(result.exception);
            Assert.IsNotNull(result.result);
            Console.WriteLine(result.result.ToString());
        }

        [TestMethod]
        public void update_account_no_password()
        {
            var result = repo.Update(new Account
            {
                f_id = 1,
                f_password = "",
                f_nickname = "ChangeUser001",
            });

            Assert.IsNull(result.exception);
            Assert.IsNotNull(result.result);
            Console.WriteLine(result.result.ToString());
        }

        [TestMethod]
        public void update_account_failed()
        {
            var result = repo.Update(new Account
            {
                f_id = 5,
                f_password = "",
                f_nickname = "",
            });

            Assert.IsNull(result.result);
            Assert.IsNull(result.exception);
        }

        [TestMethod]
        public void reset_account()
        {
            var result = repo.Reset(1, "ResetPWD001");

            Assert.IsTrue(result.isSuccess);
        }
    }
}
