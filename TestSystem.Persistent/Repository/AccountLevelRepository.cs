using Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using TestSystem.Persistent.Model;
using TestSystem.Persistent.Repository.Interface;

namespace TestSystem.Persistent.Repository
{
    public class AccountLevelRepository : IAccountLevelRepository
    {
        private string connectionString;

        public AccountLevelRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public (Exception exception, AccountLevel result) Create(int accId)
        {
            try
            {
                using (var cn = new SqlConnection(connectionString))
                {
                    var reuslt = cn.QueryFirstOrDefault<AccountLevel>(
                        "pro_setAccountLevel",
                        new
                        {
                            accId = accId,
                        },
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
