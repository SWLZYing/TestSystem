using System;
using System.Web.Http;
using TestSystem.Persistent.Model;
using TestSystem.Persistent.Repository.Interface;
using TestSystem.WebApi.Models;

namespace TestSystem.WebApi.Controllers
{
    [Route("Api/Account/{action}")]
    public class AccountController : ApiController
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountLevelRepository _accountLevelRepository;

        public AccountController(
            IAccountRepository accountRepository,
            IAccountLevelRepository accountLevelRepository)
        {
            _accountRepository = accountRepository;
            _accountLevelRepository = accountLevelRepository;
        }

        /*
            回覆代碼 0000: 成功，1111: 必填欄位缺失，2222: 資料庫新增失敗，9999: 系統錯誤 
        */

        [HttpPost]
        public CreateUserResponse CreateUser(CreateUserRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Acc))
                {
                    return new CreateUserResponse { Code = "1111", Message = "帳號需有值." };
                }

                if (string.IsNullOrWhiteSpace(request.Pwd))
                {
                    return new CreateUserResponse { Code = "1111", Message = "密碼需有值." };
                }

                var accResult = _accountRepository.Create(new Account
                {
                    f_account = request.Acc,
                    f_password = request.Pwd,
                    f_nickname = request.Nickname ?? string.Empty,
                });

                if (accResult.exception != null)
                {
                    return new CreateUserResponse { Code = "2222", Message = accResult.exception.Message };
                }

                var accLevelResult = _accountLevelRepository.Create(accResult.result.f_id);

                return accLevelResult.exception == null
                    ? new CreateUserResponse { Code = "0000", Acc = accResult.result, AccLevel = accLevelResult.result }
                    : new CreateUserResponse { Code = "2222", Message = accResult.exception.Message };
            }
            catch (Exception ex)
            {
                return new CreateUserResponse { Code = "9999", Message = ex.Message };
            }
        }
    }
}