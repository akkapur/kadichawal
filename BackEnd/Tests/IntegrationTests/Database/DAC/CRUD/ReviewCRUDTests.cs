using Database.DAC.CRUD;
using Interfaces.DataContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.IntegrationTests.Database.DAC.CRUD
{
    [TestClass]
    public class ReviewCRUDTests
    {
        private static IReviewCRUD<Review> _reviewCRUD;
        private static IOrganizationCRUD<Organization> _organizationCRUD;
        private static IUserCRUD<User> _userCRUD;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _reviewCRUD = new ReviewCRUD<Review>();
            _reviewCRUD.Initialize();

            _organizationCRUD = new OrganizationCRUD<Organization>();
            _organizationCRUD.Initialize();

            _userCRUD = new UserCRUD<User>();
            _userCRUD.Initialize();  
        }

        [TestMethod]
        public void ReviewCRUDCalled_OperationSuccessfull()
        {
            User user = new User();
            user.EmailAddress = "user@test.com";
            user.Password = "password";
            user.IsLockedOut = false;
            user.IsApproved = true;
            user.PasswordQuestion = "whats the word";
            user.PasswordAnswer = "bird is the word";
            user.IsOrganization = true;
            _userCRUD.Create(user);

            //create organization.
            Organization organization = new Organization();
            organization.UserId = user.UserId;
            organization.AddressLine1 = "line 1";
            organization.AddressLine2 = "line 2";
            organization.BusinessName = "Hello World Inc.";
            organization.BusinessType = BusinessType.Technology;
            organization.EmailAddress = "marketing@helloworldinc.com";
            organization.Country = "India";

            _organizationCRUD.Create(organization);
            organization = _organizationCRUD.Read(x => x.BusinessName == "Hello World Inc.").FirstOrDefault<Organization>();
            Assert.IsNotNull(organization);

            Review review = new Review();
            review.CreationDate = DateTime.Now;
            review.IsApproved = true;
            review.OrganizationId = organization.OrganizationId;
            review.ReviewerId = user.UserId;
            review.ReviewText = "This is a good place";
            _reviewCRUD.Create(review);

            Assert.IsNotNull(_reviewCRUD.Read(review.ReviewId));

            
        }
    }
}
