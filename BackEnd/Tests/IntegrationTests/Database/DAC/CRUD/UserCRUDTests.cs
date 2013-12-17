using Database.DAC.CRUD;
using Interfaces.DataContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.IntegrationTests.Database.DAC.CRUD
{
    [TestClass]
    public sealed class UserCRUDTests
    {
        private static IUserCRUD<User> _userCRUD;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _userCRUD = new UserCRUD<User>();
            _userCRUD.Initialize();           
        }

        [TestMethod]
        public void UserCRUDCalled_OperationSuccessfull()
        {
            ////Create
            User user = new User();
            user.EmailAddress = "user@test.com";
            user.Password = "password";
            user.IsLockedOut = false;
            user.IsApproved = true;
            user.PasswordQuestion = "whats the word";
            user.PasswordAnswer = "bird is the word";
            _userCRUD.Create(user);
            User result = _userCRUD.Read(x => x.UserId == user.UserId).FirstOrDefault<User>();
            Assert.IsNotNull(result);
            result = _userCRUD.Read(x => x.UserName == user.UserName).FirstOrDefault<User>();
            Assert.IsNotNull(result);
            Assert.IsTrue(String.Compare(result.UserName, "tuser", true) == 0);

            ////Update
            user.Password = "betterPa$$w0rd";
            _userCRUD.Update(user);
            result = _userCRUD.Read(x => x.UserId == user.UserId).FirstOrDefault<User>();
            Assert.IsNotNull(result);
            Assert.IsTrue(String.Compare(result.Password, "betterPa$$w0rd", true) == 0);

            ////Delete
            string id = user.UserId;
            _userCRUD.Delete(user.UserId);
            result = _userCRUD.Read(id);
            Assert.IsNull(result);
        }
    }
}
