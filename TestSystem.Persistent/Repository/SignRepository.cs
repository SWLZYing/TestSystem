using Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using TestSystem.Persistent.Repository.Interface;

namespace TestSystem.Persistent.Repository
{
    public class SignRepository : ISignRepository
    {
        private string connectionString;

        public SignRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public (Exception exception, bool isSuccess) SignIn(string acc, string pwd)
        {
            try
            {
                using (var cn = new SqlConnection(connectionString))
                {
                    var result = cn.Execute(
                        "pro_signIn",
                        new
                        {
                            acc = acc,
                            pwd = pwd,
                        },
                        commandType: CommandType.StoredProcedure);

                    return (null, IsSuccess(result));
                }
            }
            catch (Exception ex)
            {
                return (ex, false);
            }
        }

        public (Exception exception, bool isSuccess) SignOut()
        {
            try
            {
                // 目前無緩存機制 登出先都回傳成功
                return (null, true);
            }
            catch (Exception ex)
            {
                return (ex, false);
            }
        }

        private bool IsSuccess(int result)
        {
            return result > 0;
        }
    }
}
