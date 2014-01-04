
using IndiTownUI.Internal.Attributes;
using IndiTownUI.ReviewServiceReference;
using Interfaces.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace IndiTownUI.Controllers
{
    public class OrganizationController : Controller
    {
        //
        // GET: /Organization/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SignUp()
        {
            return View();
        }

        /// <summary>
        /// This should be used by a Business Owner to setup his/her business on IndiTown.
        /// </summary>
        /// <param name="organizationUser"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SignUp(OrganizationUser organizationUser)
        {
            try
            {
                IndiTownUI.UserServiceReference.UserServiceClient userClient = new UserServiceReference.UserServiceClient();
                UserServiceReference.User user = new UserServiceReference.User();
                user.UserName = organizationUser.User.UserName;
                user.Password = organizationUser.User.Password;
                user.EmailAddress = organizationUser.User.EmailAddress;
                user.Password = organizationUser.User.Password;
                user.PasswordQuestion = organizationUser.User.PasswordQuestion;
                user.PasswordAnswer = organizationUser.User.PasswordAnswer;
                userClient.CreateUser(user);

                if (organizationUser.Organization.BusinessType == BusinessType.Unknown)
                    organizationUser.Organization.BusinessType = BusinessType.Other;

                IndiTownUI.OrganizationServiceReference.OrganizationServiceClient organizationClient = new OrganizationServiceReference.OrganizationServiceClient();
                OrganizationServiceReference.Organization organization = new OrganizationServiceReference.Organization();
                organization.UserId = user.UserId;
                organization.AddressLine1 = organizationUser.Organization.AddressLine1;
                organization.AddressLine2 = organizationUser.Organization.AddressLine2;
                organization.BusinessHours = organizationUser.Organization.BusinessHours;
                organization.BusinessName = organizationUser.Organization.BusinessName;
                organization.City = organizationUser.Organization.City;
                organization.Country = organizationUser.Organization.Country;
                organization.EmailAddress = organizationUser.Organization.EmailAddress;
                organization.State = organizationUser.Organization.State;
                organization.BusinessType = (OrganizationServiceReference.BusinessType)(int)organizationUser.Organization.BusinessType;
                organizationClient.CreateOrganization(organization);

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        [AuthorizeUser]
        public ActionResult AddBusiness()
        {
            return View();
        }

        /// <summary>
        /// This method is to be called when a user wants to add a business/organization.
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeUser]
        public ActionResult AddBusiness(Organization organization, FormCollection formCollection)
        {
            try
            {
                if (String.IsNullOrEmpty(organization.BusinessName) || String.IsNullOrWhiteSpace(organization.BusinessName))
                    throw new ArgumentException();

                IndiTownUI.UserServiceReference.UserServiceClient userClient = new UserServiceReference.UserServiceClient();
                UserServiceReference.User user = userClient.GetUser(User.Identity.Name);
                if (user != null)
                {
                    if(organization.BusinessType == BusinessType.Unknown)
                        organization.BusinessType = BusinessType.Other;

                    IndiTownUI.OrganizationServiceReference.OrganizationServiceClient organizationClient = new OrganizationServiceReference.OrganizationServiceClient();
                    OrganizationServiceReference.Organization org = new OrganizationServiceReference.Organization();
                    org.CreatedById = user.UserId;
                    org.AddressLine1 = organization.AddressLine1;
                    org.AddressLine2 = organization.AddressLine2;
                    org.BusinessHours = organization.BusinessHours;
                    org.BusinessName = organization.BusinessName;
                    org.City = organization.City;
                    org.Country = organization.Country;
                    org.EmailAddress = organization.EmailAddress;
                    org.State = organization.State;
                    org.BusinessType = (OrganizationServiceReference.BusinessType)(int)organization.BusinessType;
                    string id = organizationClient.CreateOrganization(org);

                    if (!String.IsNullOrWhiteSpace(formCollection["ReviewText"]))
                    {
                        ReviewServiceReference.Review review = new ReviewServiceReference.Review();
                        review.OrganizationId = id;
                        review.ReviewerId = user.UserId;
                        review.CreationDate = DateTime.Now;
                        review.ReviewText = formCollection["ReviewText"];
                        IndiTownUI.ReviewServiceReference.ReviewServiceClient reviewClient = new ReviewServiceReference.ReviewServiceClient();
                        reviewClient.CreateReview(review);
                    }
                }


                return RedirectToAction("Index", "Home");
            }
            catch(Exception e)
            {
                ModelState.AddModelError("", String.Format("Error Adding Business. {0}", e.Message));
                return View(organization);
            }
        }

        public ActionResult ViewBusiness(string id)
        {
            IndiTownUI.OrganizationServiceReference.OrganizationServiceClient organizationClient = new OrganizationServiceReference.OrganizationServiceClient();
            OrganizationServiceReference.OrganizationProfile profile = organizationClient.GetOrgranizationProfile(id);

            Interfaces.DataContracts.Organization organization = new Interfaces.DataContracts.Organization
            {
                UserId = profile.Organization.UserId,
                AddressLine1 = profile.Organization.AddressLine1,
                AddressLine2 = profile.Organization.AddressLine2,
                BusinessHours = profile.Organization.BusinessHours,
                BusinessType = (Interfaces.DataContracts.BusinessType)(int)profile.Organization.BusinessType,
                BusinessName = profile.Organization.BusinessName,
                City = profile.Organization.City,
                Country = profile.Organization.Country,
                EmailAddress = profile.Organization.EmailAddress,
                State = profile.Organization.State,
                OrganizationId = profile.Organization.OrganizationId
            };

            IndiTownUI.ReviewServiceReference.ReviewServiceClient reviewServiceClient = new ReviewServiceClient();
            IEnumerable<ReviewServiceReference.Review> businessReviews = reviewServiceClient.GetBusinessReview(id);

            List<Interfaces.DataContracts.Review> reviews = new List<Interfaces.DataContracts.Review>();
            foreach (ReviewServiceReference.Review review in businessReviews)
            {
                Interfaces.DataContracts.Review tempReview = new Interfaces.DataContracts.Review
                {
                    ReviewId = review.ReviewId,
                    ReviewerId = review.ReviewerId,
                    ReviewText = review.ReviewText,
                    OrganizationId = review.OrganizationId
                };
                reviews.Add(tempReview);
            }

            Interfaces.DataContracts.OrganizationProfile organizationProfile = new OrganizationProfile();
            organizationProfile.Organization = organization;
            organizationProfile.Reviews = reviews.ToArray();

            return View(organizationProfile);
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }

    }
}
