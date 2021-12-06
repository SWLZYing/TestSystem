using System;
using TestSystem.Persistent.Model;

namespace TestSystem.Persistent.Repository.Interface
{
    public interface IAccountLevelRepository
    {
        /// <summary>
        /// 帳號新增
        /// </summary>
        /// <param name="acc"></param>
        /// <returns></returns>
        (Exception exception, AccountLevel result) Create(int accId);
    }
}
