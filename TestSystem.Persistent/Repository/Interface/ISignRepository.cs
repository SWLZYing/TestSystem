using System;

namespace TestSystem.Persistent.Repository.Interface
{
    public interface ISignRepository
    {
        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="acc"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        (Exception exception, bool isSuccess) SignIn(string acc, string pwd);

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        (Exception exception, bool isSuccess) SignOut();
    }
}
