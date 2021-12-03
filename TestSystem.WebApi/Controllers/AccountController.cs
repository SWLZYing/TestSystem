using System;
using System.Web.Configuration;
using System.Web.Http;
using TestSystem.Persistent.Model;
using TestSystem.Persistent.Repository;
using TestSystem.Persistent.Repository.Interface;
using TestSystem.WebApi.Models;

namespace TestSystem.WebApi.Controllers
{
    [Route("Account/{action}")]
    public class AccountController : ApiController
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController()
        {
            _accountRepository = new AccountRepository(WebConfigurationManager.ConnectionStrings["MSSQLContext"].ConnectionString);
        }

        [HttpPost]
        public CreateUserResponse CreateUser(CreateUserRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Acc))
                {
                    return new CreateUserResponse { Code = "6666", Message = "帳號需有值." };
                }
                if (string.IsNullOrWhiteSpace(request.Pwd))
                {
                    return new CreateUserResponse { Code = "6666", Message = "密碼需有值." };
                }

                var result = _accountRepository.Create(new Account
                {
                    f_account = request.Acc,
                    f_password = request.Pwd,
                    f_nickname = request.Nickname ?? "",
                });

                return result.exception == null
                    ? new CreateUserResponse { Code = "0000", Data = result.result }
                    : new CreateUserResponse { Code = "6666", Message = result.exception.Message };
            }
            catch (Exception ex)
            {
                return new CreateUserResponse { Code = "6666", Message = ex.Message };
            }
        }
    }
}