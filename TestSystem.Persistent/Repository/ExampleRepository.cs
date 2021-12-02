
namespace TestSystem.Persistent.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using Dapper;
    using TestSystem.Persistent.Model;
    using TestSystem.Persistent.Repository.Interface;

    public class ExampleRepository : IExampleRepository
    {
        private string connectionString;

        public ExampleRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public (Exception exception, IEnumerable<Example> examples) Query()
        {
            try
            {
                using (var cn = new SqlConnection(this.connectionString))
                {
                    var reuslt = cn.Query<Example>(
                        "pro_getExample",
                        commandType: CommandType.StoredProcedure);

                    return (null, reuslt);
                }
            }
            catch (Exception ex)
            {
                return (ex, null);
            }
        }
    }
}
