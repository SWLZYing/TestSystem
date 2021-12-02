
namespace TestSystem.Persistent.Tests.Repository
{
    using System.Data.SqlClient;
    using Dapper;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TestSystem.Persistent.Repository;
    using TestSystem.Persistent.Repository.Interface;

    [TestClass]
    public class ExampleRepositoryTests
    {
        const string connectionString = @"Data Source=localhost;Integrated Security=True;database=TestSystem;TrustServerCertificate=True";

        private IExampleRepository repo;

        [TestInitialize]
        public void Init()
        {
            using (var cn = new SqlConnection(connectionString))
            {
                // 清空表確保每次單元測試都是獨立的
                string sqlStr = @"TRUNCATE TABLE t_example";
                cn.Execute(sqlStr);
            }

            this.repo = new ExampleRepository(connectionString);
        }

        [TestMethod]
        public void 取資料測試()
        {
            var queryResult = this.repo.Query();

            Assert.IsNull(queryResult.exception);
            Assert.IsNotNull(queryResult.examples);
        }
    }
}
