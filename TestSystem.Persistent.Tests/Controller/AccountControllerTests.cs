using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestSystem.WebApi.Controllers;
using TestSystem.WebApi.Models;

namespace TestSystem.Persistent.Tests.Controller
{
    [TestClass]
    public class AccountControllerTests
    {
        private AccountController _controller;

        [TestInitialize]
        public void Into()
        {
            _controller = new AccountController();
        }

        [TestMethod]
        public void create_user_no_acc()
        {
            var response = _controller.CreateUser(new CreateUserRequest
            {
                Acc = "",
                Pwd = "test",
                Nickname = "test",
            });

            Assert.Equals("0000", response.Code);
        }
    }
}
