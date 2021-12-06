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
            回覆代碼 
            0000: 成功
            1111: 必填欄位缺失
            2222: 資料庫新增失敗
            3333: 查無資料
            4444: 密碼驗證錯誤
            9999: 系統錯誤 
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

        [HttpPost]
        public QueryUserResponse QueryUser(QueryUserRequest request)
        {
            try
            {
                var result = _accountRepository.Query(request.AccId);

                if (result.exception != null)
                {
                    return new QueryUserResponse { Code = "9999", Message = result.exception.Message };
                }

                if (result.result == null)
                {
                    return new QueryUserResponse { Code = "3333", Message = "查無此帳號." };
                }

                return new QueryUserResponse { Code = "0000", Acc = result.result };
            }
            catch (Exception ex)
            {
                return new QueryUserResponse { Code = "9999", Message = ex.Message };
            }
        }

        [HttpPost]
        public UpdateUserResponse UpdateUser(UpdateUserRequest request)
        {
            try
            {
                // 變更密碼需驗證舊密碼是否正確
                if (!string.IsNullOrWhiteSpace(request.Pwd))
                {
                    var acc = _accountRepository.Query(request.Id);

                    if (acc.exception != null)
                    {
                        return new UpdateUserResponse { Code = "9999", Message = acc.exception.Message };
                    }

                    if (acc.result == null)
                    {
                        return new UpdateUserResponse { Code = "3333", Message = "查無此帳號." };
                    }

                    if (acc.result.f_password != request.OldPwd)
                    {
                        return new UpdateUserResponse { Code = "4444", Message = "密碼驗證錯誤." };
                    }
                }

                var result = _accountRepository.Update(new Account
                {
                    f_id = request.Id,
                    f_password = request.Pwd,
                    f_nickname = request.Nickname,
                });

                if (result.exception != null)
                {
                    return new UpdateUserResponse { Code = "9999", Message = result.exception.Message };
                }

                if (result.result == null)
                {
                    return new UpdateUserResponse { Code = "3333", Message = "查無此帳號." };
                }

                return new UpdateUserResponse { Code = "0000", Acc = result.result };
            }
            catch (Exception ex)
            {
                return new UpdateUserResponse { Code = "9999", Message = ex.Message };
            }
        }

        [HttpPost]
        public ResetPwdResponse ResetPwd(ResetPwdRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Pwd))
                {
                    return new ResetPwdResponse { Code = "1111", Message = "密碼需有值." };
                }

                var acc = _accountRepository.Query(request.Id);

                if (acc.exception != null)
                {
                    return new ResetPwdResponse { Code = "9999", Message = acc.exception.Message };
                }

                if (acc.result == null)
                {
                    return new ResetPwdResponse { Code = "3333", Message = "查無此帳號." };
                }

                if (acc.result.f_password != request.OldPwd)
                {
                    return new ResetPwdResponse { Code = "4444", Message = "密碼驗證錯誤." };
                }

                var result = _accountRepository.Reset(request.Id, request.Pwd);

                if (result.exception != null)
                {
                    return new ResetPwdResponse { Code = "9999", Message = result.exception.Message };
                }

                return new ResetPwdResponse { Code = "0000", IsSuccess = result.isSuccess };
            }
            catch (Exception ex)
            {
                return new ResetPwdResponse { Code = "9999", Message = ex.Message };
            }
        }
    }
}