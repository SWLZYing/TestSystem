using System;
using System.Web.Http;
using TestSystem.Persistent.Repository.Interface;
using TestSystem.WebApi.Enums;
using TestSystem.WebApi.Models;

namespace TestSystem.WebApi.Controllers
{
    [Route("Api/Sign/{action}")]
    public class SignController : ApiController
    {
        private readonly ISignRepository _signRepository;

        public SignController(ISignRepository signRepository)
        {
            _signRepository = signRepository;
        }

        [HttpPost]
        public SignInResponse SignIn(SignInRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Acc))
                {
                    return new SignInResponse { Code = (int)ErrorCodeType.FieldsMiss, Message = "帳號需有值." };
                }

                if (string.IsNullOrWhiteSpace(request.Pwd))
                {
                    return new SignInResponse { Code = (int)ErrorCodeType.FieldsMiss, Message = "密碼需有值." };
                }

                var result = _signRepository.SignIn(request.Acc, request.Pwd);

                if (result.exception != null)
                {
                    return new SignInResponse { Code = (int)ErrorCodeType.SystemError, Message = result.exception.Message };
                }

                switch (result.result.Item1)
                {
                    case 0:
                        return new SignInResponse { Code = (int)ErrorCodeType.NotFound, Message = "帳號/密碼錯誤" };
                    case 1:
                        return new SignInResponse { Code = (int)ErrorCodeType.Success, Acc = result.result.Item2 };
                    case 2:
                        return new SignInResponse { Code = (int)ErrorCodeType.PwdError, Message = "帳號/密碼錯誤" };
                    default:
                        return new SignInResponse { Code = (int)ErrorCodeType.SystemError, Message = "系統錯誤" };
                }
            }
            catch (Exception ex)
            {
                return new SignInResponse { Code = (int)ErrorCodeType.SystemError, Message = ex.Message };
            }
        }

        [HttpPost]
        public SignOutResponse SignOut(SignOutRequest request)
        {
            try
            {
                var result = _signRepository.SignOut(request.AccId);

                return new SignOutResponse { Code = (int)ErrorCodeType.Success, IsSuccess = result.isSuccess };
            }
            catch (Exception ex)
            {
                return new SignOutResponse { Code = (int)ErrorCodeType.SystemError, Message = ex.Message };
            }
        }
    }
}