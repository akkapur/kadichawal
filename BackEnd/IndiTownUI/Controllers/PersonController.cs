
using Interfaces.DataContracts;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IndiTownUI.Controllers
{
    public class PersonController : Controller
    {
        //
        // GET: /Person/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(PersonUser personUser)
        {
            try
            {
                IndiTownUI.UserServiceReference.UserServiceClient userClient = new UserServiceReference.UserServiceClient();
                UserServiceReference.User user = new UserServiceReference.User();
                user.UserName = personUser.User.UserName;
                user.Password = personUser.User.Password;
                user.EmailAddress = personUser.User.EmailAddress;
                user.Password = personUser.User.Password;
                user.PasswordQuestion = personUser.User.PasswordQuestion;
                user.PasswordAnswer = personUser.User.PasswordAnswer;
                string userid = userClient.CreateUser(user);

                IndiTownUI.PersonServiceReference.PersonServiceClient personClient = new PersonServiceReference.PersonServiceClient();
                PersonServiceReference.Person person = new PersonServiceReference.Person();
                person.FullName = personUser.Person.FullName;
                person.City = personUser.Person.City;
                //person.Gender = personUser.Person.Gender;
                person.Interest = personUser.Person.Interest;
                person.AboutMe = personUser.Person.AboutMe;
                person.UserId = userid;
                personClient.CreatePerson(person);

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }
    }
}
