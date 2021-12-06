using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using TestSystem.Persistent.Model;
using TestSystem.Persistent.Repository.Interface;
using TestSystem.WebApi.Controllers;
using TestSystem.WebApi.Models;

namespace TestSystem.Persistent.Tests.Controller
{
    [TestClass]
    public class AccountControllerTests
    {
        private Mock<IAccountRepository> _accRepo;
        private Mock<IAccountLevelRepository> _accLvRepo;

        [TestInitialize]
        public void Into()
        {
            _accRepo = new Mock<IAccountRepository>();
            _accLvRepo = new Mock<IAccountLevelRepository>();
        }

        [TestMethod]
        public void create_user_no_acc()
        {
            var controller = new AccountController(_accRepo.Object, _accLvRepo.Object);

            var response = controller.CreateUser(new CreateUserRequest
            {
                Acc = string.Empty,
                Pwd = "test",
                Nickname = "test",
            });

            Assert.AreEqual("1111", response.Code);
            Console.WriteLine(response.Message);
        }

        [TestMethod]
        public void create_user_no_pwd()
        {
            var controller = new AccountController(_accRepo.Object, _accLvRepo.Object);

            var response = controller.CreateUser(new CreateUserRequest
            {
                Acc = "test",
                Pwd = string.Empty,
                Nickname = "test",
            });

            Assert.AreEqual("1111", response.Code);
            Console.WriteLine(response.Message);
        }

        [TestMethod]
        public void create_user_success()
        {
            _accRepo.Setup(s => s.Create(It.IsAny<Account>()))
                .Returns((null, new Account { f_id = 1 }));

            _accLvRepo.Setup(s => s.Create(It.IsAny<int>()))
                .Returns((null, new AccountLevel { f_id = 1 }));

            var controller = new AccountController(_accRepo.Object, _accLvRepo.Object);

            var response = controller.CreateUser(new CreateUserRequest
            {
                Acc = "test",
                Pwd = "test",
                Nickname = "test",
            });

            Assert.AreEqual("0000", response.Code);
            Console.WriteLine($"Acc:{response.Acc}, AccLevel:{response.AccLevel}");
        }

        [TestMethod]
        public void create_user_failed()
        {
            _accRepo.Setup(s => s.Create(It.IsAny<Account>()))
                .Returns((new Exception(), null));

            var controller = new AccountController(_accRepo.Object, _accLvRepo.Object);

            var response = controller.CreateUser(new CreateUserRequest
            {
                Acc = "test",
                Pwd = "test",
                Nickname = "test",
            });

            Assert.AreEqual("2222", response.Code);
            Console.WriteLine(response.Message);
        }
    }
}
