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

        [TestMethod]
        public void query_user_success()
        {
            _accRepo.Setup(s => s.Query(It.IsAny<int>()))
                .Returns((null, new Account { f_id = 1 }));

            var controller = new AccountController(_accRepo.Object, _accLvRepo.Object);

            var response = controller.QueryUser(new QueryUserRequest
            {
                AccId = 1
            });

            Assert.AreEqual("0000", response.Code);
            Console.WriteLine(response.Acc.ToString());
        }

        [TestMethod]
        public void query_user_failed()
        {
            _accRepo.Setup(s => s.Query(It.IsAny<int>()))
                .Returns((null, null));

            var controller = new AccountController(_accRepo.Object, _accLvRepo.Object);

            var response = controller.QueryUser(new QueryUserRequest
            {
                AccId = 3
            });

            Assert.AreEqual("3333", response.Code);
            Console.WriteLine(response.Message);
        }

        [TestMethod]
        public void query_user_system_error()
        {
            _accRepo.Setup(s => s.Query(It.IsAny<int>()))
                .Returns((new Exception(), null));

            var controller = new AccountController(_accRepo.Object, _accLvRepo.Object);

            var response = controller.QueryUser(new QueryUserRequest
            {
                AccId = 1
            });

            Assert.AreEqual("9999", response.Code);
            Console.WriteLine(response.Message);
        }

        [TestMethod]
        public void update_user_success()
        {
            _accRepo.Setup(s => s.Update(It.IsAny<Account>()))
                .Returns((null, new Account { f_id = 1 }));

            var controller = new AccountController(_accRepo.Object, _accLvRepo.Object);

            var response = controller.UpdateUser(new UpdateUserRequest
            {
                Id = 1
            });

            Assert.AreEqual("0000", response.Code);
            Console.WriteLine(response.Acc.ToString());
        }

        [TestMethod]
        public void update_user_failed()
        {
            _accRepo.Setup(s => s.Update(It.IsAny<Account>()))
                .Returns((null, null));

            var controller = new AccountController(_accRepo.Object, _accLvRepo.Object);

            var response = controller.UpdateUser(new UpdateUserRequest
            {
                Id = 3
            });

            Assert.AreEqual("3333", response.Code);
            Console.WriteLine(response.Message);
        }

        [TestMethod]
        public void update_user_system_error()
        {
            _accRepo.Setup(s => s.Update(It.IsAny<Account>()))
                .Returns((new Exception(), null));

            var controller = new AccountController(_accRepo.Object, _accLvRepo.Object);

            var response = controller.UpdateUser(new UpdateUserRequest
            {
                Id = 1
            });

            Assert.AreEqual("9999", response.Code);
            Console.WriteLine(response.Message);
        }

        [TestMethod]
        public void update_user_with_pwd_success()
        {
            _accRepo.Setup(s => s.Query(It.IsAny<int>()))
                .Returns((null, new Account { f_password = "old" }));
            _accRepo.Setup(s => s.Update(It.IsAny<Account>()))
                .Returns((null, new Account { f_id = 1 }));

            var controller = new AccountController(_accRepo.Object, _accLvRepo.Object);

            var response = controller.UpdateUser(new UpdateUserRequest
            {
                Id = 1,
                Pwd = "new",
                OldPwd = "old",
            });

            Assert.AreEqual("0000", response.Code);
            Console.WriteLine(response.Acc.ToString());
        }

        [TestMethod]
        public void update_user_with_pwd_failed()
        {
            _accRepo.Setup(s => s.Query(It.IsAny<int>()))
                .Returns((null, new Account { f_password = "old" }));

            var controller = new AccountController(_accRepo.Object, _accLvRepo.Object);

            var response = controller.UpdateUser(new UpdateUserRequest
            {
                Id = 1,
                Pwd = "new",
                OldPwd = "notOld",
            });

            Assert.AreEqual("4444", response.Code);
            Console.WriteLine(response.Message);
        }

        [TestMethod]
        public void reset_pwd_success()
        {
            _accRepo.Setup(s => s.Query(It.IsAny<int>()))
                .Returns((null, new Account { f_password = "old" }));
            _accRepo.Setup(s => s.Reset(It.IsAny<int>(), It.IsAny<string>()))
                .Returns((null, true));

            var controller = new AccountController(_accRepo.Object, _accLvRepo.Object);

            var response = controller.ResetPwd(new ResetPwdRequest
            {
                Id = 1,
                Pwd = "new",
                OldPwd = "old",
            });

            Assert.AreEqual("0000", response.Code);
            Assert.IsTrue(response.IsSuccess);
        }

        [TestMethod]
        public void reset_pwd_is_null()
        {
            var controller = new AccountController(_accRepo.Object, _accLvRepo.Object);

            var response = controller.ResetPwd(new ResetPwdRequest
            {
                Id = 1,
                Pwd = string.Empty,
                OldPwd = "old",
            });

            Assert.AreEqual("1111", response.Code);
            Console.WriteLine(response.Message);
        }

        [TestMethod]
        public void reset_pwd_old_pwd_is_different()
        {
            _accRepo.Setup(s => s.Query(It.IsAny<int>()))
                .Returns((null, new Account { f_password = "old" }));

            var controller = new AccountController(_accRepo.Object, _accLvRepo.Object);

            var response = controller.ResetPwd(new ResetPwdRequest
            {
                Id = 1,
                Pwd = "new",
                OldPwd = "notOld",
            });

            Assert.AreEqual("4444", response.Code);
            Console.WriteLine(response.Message);
        }

        [TestMethod]
        public void reset_pwd_id_is_not_exist()
        {
            _accRepo.Setup(s => s.Query(It.IsAny<int>()))
                .Returns((null, null));

            var controller = new AccountController(_accRepo.Object, _accLvRepo.Object);

            var response = controller.ResetPwd(new ResetPwdRequest
            {
                Id = 1,
                Pwd = "new",
                OldPwd = "old",
            });

            Assert.AreEqual("3333", response.Code);
            Console.WriteLine(response.Message);
        }
    }
}
