using Database.DAC.CRUD;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using Interfaces.DataContracts;
using MongoDB.Bson;

namespace Tests.IntegrationTests.Database.DAC.CRUD
{
    [TestClass]
    public sealed class PersonCRUDTests
    {
        private static IPersonCRUD<Person> _personCRUD;
        private static IUserCRUD<User> _userCRUD;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _personCRUD = new PersonCRUD<Person>();
            _personCRUD.Initialize();

            _userCRUD = new UserCRUD<User>();
            _userCRUD.Initialize();  
        }

        [TestMethod]
        public void PersonCRUDCalled_OperationSuccessfull()
        {
            ////Create User
            User user = new User();
            user.EmailAddress = "user@test.com";
            user.Password = "password";
            user.IsLockedOut = false;
            user.IsApproved = true;
            user.PasswordQuestion = "whats the word";
            user.PasswordAnswer = "bird is the word";
            _userCRUD.Create(user);

            ////Create
            Person person = new Person();
            person.UserId = user.UserId;
            person.FullName = "Test Person";
            //person.Gender = Gender.Female;
            person.AboutMe = "I am just test";
            person.Interest = "testing.";
            _personCRUD.Create(person);
            Person result = _personCRUD.Read(x => x.PersonId == person.PersonId).FirstOrDefault<Person>();
            Assert.IsNotNull(result);
            Assert.IsTrue(String.Compare(result.FullName, "Test Person", true) == 0);

            ////Update
            person.FullName = "Test Person III";
            _personCRUD.Update(person);
            result = _personCRUD.Read(x => x.PersonId == person.PersonId).FirstOrDefault<Person>();
            Assert.IsNotNull(result);
            Assert.IsTrue(String.Compare(result.FullName, "Test Person III", true) == 0);

            ////Delete
            string id = person.PersonId;
            _personCRUD.Delete(person.PersonId);
            result = _personCRUD.Read(id);
            Assert.IsNull(result);
        }
    }
}
