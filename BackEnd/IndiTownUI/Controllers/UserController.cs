
using IndiTownUI.Internal.Attributes;
using Interfaces.DataContracts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace IndiTownUI.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /User/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /User/Create

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(User user)
        {
            try
            {
                IndiTownUI.UserServiceReference.UserServiceClient client = new UserServiceReference.UserServiceClient();
                UserServiceReference.User usr = new UserServiceReference.User();
                usr.UserName = user.UserName;
                usr.Password = user.Password;
                usr.EmailAddress = user.EmailAddress;
                usr.Password = user.Password;
                usr.PasswordQuestion = user.PasswordQuestion;
                usr.PasswordAnswer = user.PasswordAnswer;
                usr.IsOrganization = bool.Parse(Request.Form.GetValues("IsOrganization")[0]);

                client.CreateUser(usr);
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            IndiTownUI.UserServiceReference.UserServiceClient client = new UserServiceReference.UserServiceClient();
            client.SignOutUser(User.Identity.Name);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult SignIn(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(User user, string returnUrl)
        {
            IndiTownUI.UserServiceReference.UserServiceClient client = new UserServiceReference.UserServiceClient();
            if (client.AuthenticateUser(user.UserName, user.Password))
            {
                FormsAuthentication.SetAuthCookie(user.UserName, true);
                return RedirectToLocal(returnUrl);
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(user);
        }

        [HttpGet]
        [AuthorizeUser]
        public ActionResult UserProfile()
        {
            IndiTownUI.UserServiceReference.UserServiceClient userSvcClient = new UserServiceReference.UserServiceClient();
            UserServiceReference.User user = userSvcClient.GetUser(User.Identity.Name);
            if (!user.IsOrganization) //not an oranization
            {
                IndiTownUI.PersonServiceReference.IPersonService personSvcClient = new PersonServiceReference.PersonServiceClient();
                PersonServiceReference.PersonProfile profile = personSvcClient.GetPersonProfile(user.UserId);
                return View(profile);
            }
            return View();
        }

        [HttpPost]
        [AuthorizeUser]
        public ActionResult UserProfile(PersonProfile personProfile)
        {
            IndiTownUI.PersonServiceReference.PersonServiceClient personSvcClient = new PersonServiceReference.PersonServiceClient();
            PersonServiceReference.Person person = new PersonServiceReference.Person();
            person.UserId = personProfile.Person.UserId;
            person.PersonId = personProfile.Person.PersonId;
            person.FullName = personProfile.Person.FullName;
            person.Gender = (PersonServiceReference.Gender)(int)personProfile.Person.Gender;
            person.City = personProfile.Person.City;
            person.Interest = personProfile.Person.Interest;
            person.AboutMe = personProfile.Person.AboutMe;
            personSvcClient.UpdatePerson(person);
            return RedirectToAction("UserProfile");
        }

        //
        // GET: /User/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /User/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /User/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //TODO: Add some validation so that an unsupported culture is not sent.
        public ActionResult ChangeCulture(string lang, string returnUrl)
        {
            Session["Culture"] = new CultureInfo(lang);
            return Redirect(returnUrl);
        }
    }
}
