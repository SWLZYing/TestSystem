using Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using TestSystem.Persistent.Model;
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

        public (Exception exception, (int, Account) result) SignIn(string acc, string pwd)
        {
            try
            {
                using (var cn = new SqlConnection(connectionString))
                {
                    var result = cn.QueryMultiple(
                        "pro_signIn",
                        new
                        {
                            acc = acc,
                            pwd = pwd,
                        },
                        commandType: CommandType.StoredProcedure);

                    var readStatus = result.ReadFirstOrDefault<int>();

                    // 登入失敗 只回傳狀態碼
                    if (readStatus != 1)
                    {
                        return (null, (readStatus, null));
                    }

                    // 登入成功 回傳使用者資訊
                    var readAcc = result.ReadFirstOrDefault<Account>();

                    return (null, (readStatus, readAcc));
                }
            }
            catch (Exception ex)
            {
                return (ex, (0, null));
            }
        }

        public (Exception exception, bool isSuccess) SignOut(int accId)
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
    }
}
