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

                                  EXEC pro_setAccount 'USER001', 'PWD001', 'User001'";
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

            Assert.IsTrue(result.isSuccess);
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

            Assert.IsFalse(result.isSuccess);
        }

        [TestMethod]
        public void get_account()
        {
            var result = repo.Query(1);

            Assert.IsNull(result.exception);
            Assert.IsNotNull(result.account);
            Console.WriteLine(result.account.ToString());
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
            
            Assert.IsTrue(result.isSuccess);
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

            Assert.IsTrue(result.isSuccess);
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

            Assert.IsFalse(result.isSuccess);
        }

        [TestMethod]
        public void reset_account()
        {
            var result = repo.Reset(1, "ResetPWD001");

            Assert.IsTrue(result.isSuccess);
        }
    }
}
