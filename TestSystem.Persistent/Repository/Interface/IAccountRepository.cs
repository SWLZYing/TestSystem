using System;
using TestSystem.Persistent.Model;

namespace TestSystem.Persistent.Repository.Interface
{
    public interface IAccountRepository
    {
        /// <summary>
        /// 帳號新增
        /// </summary>
        /// <param name="acc"></param>
        /// <returns></returns>
        (Exception exception, bool isSuccess) Create(Account acc);

        /// <summary>
        /// 帳號查詢
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        (Exception exception, Account account) Query(int id);

        /// <summary>
        /// 帳號更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pwd"></param>
        /// <param name="nickname"></param>
        /// <returns></returns>
        (Exception exception, bool isSuccess) Update(Account acc);

        /// <summary>
        /// 重置密碼
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        (Exception exception, bool isSuccess) Reset(int id, string pwd);
    }
}
