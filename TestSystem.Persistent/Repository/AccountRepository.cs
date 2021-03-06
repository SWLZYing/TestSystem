using Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using TestSystem.Persistent.Model;
using TestSystem.Persistent.Repository.Interface;

namespace TestSystem.Persistent.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private string connectionString;

        public AccountRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public (Exception exception, Account result) Create(Account info)
        {
            try
            {
                using (var cn = new SqlConnection(connectionString))
                {
                    var reuslt = cn.QueryFirstOrDefault<Account>(
                        "pro_setAccount",
                        new
                        {
                            acc = info.f_account,
                            pwd = info.f_password,
                            nickname = info.f_nickname,
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

        public (Exception exception, Account result) Query(int id)
        {
            try
            {
                using (var cn = new SqlConnection(connectionString))
                {
                    var result = cn.QueryFirstOrDefault<Account>(
                        "pro_getAccount",
                        new
                        {
                            id = id,
                        },
                        commandType: CommandType.StoredProcedure);

                    return (null, result);
                }
            }
            catch (Exception ex)
            {
                return (ex, null);
            }
        }

        public (Exception exception, Account result) Update(Account acc)
        {
            try
            {
                using (var cn = new SqlConnection(connectionString))
                {
                    var reuslt = cn.QueryFirstOrDefault<Account>(
                        "pro_updateAccount",
                        new
                        {
                            id = acc.f_id,
                            pwd = acc.f_password,
                            nickname = acc.f_nickname,
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

        public (Exception exception, bool isSuccess) Reset(int id, string pwd)
        {
            try
            {
                using (var cn = new SqlConnection(connectionString))
                {
                    var reuslt = cn.Execute(
                        "pro_resetAccount",
                        new
                        {
                            id = id,
                            pwd = pwd,
                        },
                        commandType: CommandType.StoredProcedure);

                    return (null, IsSuccess(reuslt));
                }
            }
            catch (Exception ex)
            {
                return (ex, false);
            }
        }

        private static bool IsSuccess(int reuslt)
        {
            return reuslt > 0;
        }
    }
}
