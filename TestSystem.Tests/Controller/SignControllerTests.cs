using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TestSystem.Persistent.Model;
using TestSystem.Persistent.Repository.Interface;
using TestSystem.WebApi.Controllers;
using TestSystem.WebApi.Enums;
using TestSystem.WebApi.Models;

namespace TestSystem.Persistent.Tests.Controller
{
    [TestClass]
    public class SignControllerTests
    {
        private Mock<ISignRepository> _signRepo;

        [TestInitialize]
        public void Into()
        {
            _signRepo = new Mock<ISignRepository>();
        }

        [TestMethod]
        public void sign_in_success()
        {
            _signRepo.Setup(s => s.SignIn(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((null, (1, new Account { f_id = 1 })));

            var controller = new SignController(_signRepo.Object);

            var response = controller.SignIn(new SignInRequest { Acc = "USER001", Pwd = "PWD001" });

            Assert.AreEqual((int)ErrorCodeType.Success, response.Code);
            System.Console.WriteLine(response.Acc.ToString());
        }

        [TestMethod]
        public void sign_in_no_acc()
        {
            var controller = new SignController(_signRepo.Object);

            var response = controller.SignIn(new SignInRequest { Acc = string.Empty, Pwd = "PWD001" });

            Assert.AreEqual((int)ErrorCodeType.FieldsMiss, response.Code);
            System.Console.WriteLine(response.Message);
        }

        [TestMethod]
        public void sign_in_no_pwd()
        {
            var controller = new SignController(_signRepo.Object);

            var response = controller.SignIn(new SignInRequest { Acc = "USER001", Pwd = string.Empty });

            Assert.AreEqual((int)ErrorCodeType.FieldsMiss, response.Code);
            System.Console.WriteLine(response.Message);
        }

        [TestMethod]
        public void sign_in_acc_error()
        {
            _signRepo.Setup(s => s.SignIn(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((null, (0, null)));

            var controller = new SignController(_signRepo.Object);

            var response = controller.SignIn(new SignInRequest { Acc = "errorAcc", Pwd = "PWD001" });

            Assert.AreEqual((int)ErrorCodeType.NotFound, response.Code);
            System.Console.WriteLine(response.Message);
        }

        [TestMethod]
        public void sign_in_pwd_error()
        {
            _signRepo.Setup(s => s.SignIn(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((null, (2, null)));

            var controller = new SignController(_signRepo.Object);

            var response = controller.SignIn(new SignInRequest { Acc = "USER0001", Pwd = "errorPwd" });

            Assert.AreEqual((int)ErrorCodeType.PwdError, response.Code);
            System.Console.WriteLine(response.Message);
        }
    }
}
