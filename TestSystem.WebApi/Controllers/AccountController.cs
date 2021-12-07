using System;
using System.Web.Http;
using TestSystem.Persistent.Model;
using TestSystem.Persistent.Repository.Interface;
using TestSystem.WebApi.Enums;
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

        [HttpPost]
        public CreateUserResponse CreateUser(CreateUserRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Acc))
                {
                    return new CreateUserResponse { Code = (int)ErrorCodeType.FieldsMiss, Message = "帳號需有值." };
                }

                if (string.IsNullOrWhiteSpace(request.Pwd))
                {
                    return new CreateUserResponse { Code = (int)ErrorCodeType.FieldsMiss, Message = "密碼需有值." };
                }

                var accResult = _accountRepository.Create(new Account
                {
                    f_account = request.Acc,
                    f_password = request.Pwd,
                    f_nickname = request.Nickname ?? string.Empty,
                });

                if (accResult.exception != null)
                {
                    return new CreateUserResponse { Code = (int)ErrorCodeType.DBError, Message = accResult.exception.Message };
                }

                var accLevelResult = _accountLevelRepository.Create(accResult.result.f_id);

                return accLevelResult.exception == null
                    ? new CreateUserResponse { Code = (int)ErrorCodeType.Success, Acc = accResult.result, AccLevel = accLevelResult.result }
                    : new CreateUserResponse { Code = (int)ErrorCodeType.DBError, Message = accResult.exception.Message };
            }
            catch (Exception ex)
            {
                return new CreateUserResponse { Code = (int)ErrorCodeType.SystemError, Message = ex.Message };
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
                    return new QueryUserResponse { Code = (int)ErrorCodeType.SystemError, Message = result.exception.Message };
                }

                if (result.result == null)
                {
                    return new QueryUserResponse { Code = (int)ErrorCodeType.NotFound, Message = "查無此帳號." };
                }

                return new QueryUserResponse { Code = (int)ErrorCodeType.Success, Acc = result.result };
            }
            catch (Exception ex)
            {
                return new QueryUserResponse { Code = (int)ErrorCodeType.SystemError, Message = ex.Message };
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
                        return new UpdateUserResponse { Code = (int)ErrorCodeType.SystemError, Message = acc.exception.Message };
                    }

                    if (acc.result == null)
                    {
                        return new UpdateUserResponse { Code = (int)ErrorCodeType.NotFound, Message = "查無此帳號." };
                    }

                    if (acc.result.f_password != request.OldPwd)
                    {
                        return new UpdateUserResponse { Code = (int)ErrorCodeType.PwdError, Message = "密碼驗證錯誤." };
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
                    return new UpdateUserResponse { Code = (int)ErrorCodeType.SystemError, Message = result.exception.Message };
                }

                if (result.result == null)
                {
                    return new UpdateUserResponse { Code = (int)ErrorCodeType.NotFound, Message = "查無此帳號." };
                }

                return new UpdateUserResponse { Code = (int)ErrorCodeType.Success, Acc = result.result };
            }
            catch (Exception ex)
            {
                return new UpdateUserResponse { Code = (int)ErrorCodeType.SystemError, Message = ex.Message };
            }
        }

        [HttpPost]
        public ResetPwdResponse ResetPwd(ResetPwdRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Pwd))
                {
                    return new ResetPwdResponse { Code = (int)ErrorCodeType.FieldsMiss, Message = "密碼需有值." };
                }

                var acc = _accountRepository.Query(request.Id);

                if (acc.exception != null)
                {
                    return new ResetPwdResponse { Code = (int)ErrorCodeType.SystemError, Message = acc.exception.Message };
                }

                if (acc.result == null)
                {
                    return new ResetPwdResponse { Code = (int)ErrorCodeType.NotFound, Message = "查無此帳號." };
                }

                if (acc.result.f_password != request.OldPwd)
                {
                    return new ResetPwdResponse { Code = (int)ErrorCodeType.PwdError, Message = "密碼驗證錯誤." };
                }

                var result = _accountRepository.Reset(request.Id, request.Pwd);

                if (result.exception != null)
                {
                    return new ResetPwdResponse { Code = (int)ErrorCodeType.SystemError, Message = result.exception.Message };
                }

                return new ResetPwdResponse { Code = (int)ErrorCodeType.Success, IsSuccess = result.isSuccess };
            }
            catch (Exception ex)
            {
                return new ResetPwdResponse { Code = (int)ErrorCodeType.SystemError, Message = ex.Message };
            }
        }
    }
}