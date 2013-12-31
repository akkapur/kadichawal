using Database.DAC.CRUD;
using Database.DAC.Search;
using Interfaces.DataContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.IntegrationTests.Database.DAC.Search
{
    [TestClass]
    public sealed class MongoSearchTests
    {
        private static MongoSearch _mongoSearch;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            SetUpTestData();
            _mongoSearch = new MongoSearch();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void SearchBusinessCalled_EmptySearchTerm_InvalidOperationExceptionThrown()
        {
            _mongoSearch.Search<Organization>(String.Empty, BusinessType.Unknown);          
        }

        [TestMethod]
        public void SearchBusinessCalled_CorrectItemsReturned()
        {
            List<Organization> organization = _mongoSearch.Search<Organization>("Hello", BusinessType.Unknown).ToList();
            Assert.IsTrue(organization.Count() == 2);
            Assert.IsTrue(organization.First().BusinessName.Equals("Hello World Inc"));
            Assert.IsTrue(organization.Skip(1).First().BusinessName.Equals("Eat Hello Joint"));
        }

        [TestMethod]
        public void SearchBusinessCalled_WithRestaurantType_CorrectItemReturned()
        {
            List<Organization> organization = _mongoSearch.Search<Organization>("Hello", BusinessType.Restaurant).ToList();
            Assert.IsTrue(organization.Count() == 1);
            Assert.IsTrue(organization.First().BusinessName.Equals("Eat Hello Joint"));
        }

        private static void SetUpTestData()
        {
            IOrganizationCRUD<Organization> organizationCrud = new OrganizationCRUD<Organization>();
            organizationCrud.Initialize();

            List<Organization> organizations = new List<Organization>();

            //Add a business with hello in the name of type technology.
            Organization organization = new Organization
            {
                AddressLine1 = "line 1",
                AddressLine2 = "line 2",
                BusinessName = String.Format("Hello World Inc"),
                BusinessType = BusinessType.Technology,
                EmailAddress = "marketing@helloworldinc.com",
                Country = "India"
            };
            organizations.Add(organization);

            //Add a business with hello in the name of type restaurant.
            organization = new Organization
            {
                AddressLine1 = "line 1",
                AddressLine2 = "line 2",
                BusinessName = String.Format("Eat Hello Joint"),
                BusinessType = BusinessType.Restaurant,
                EmailAddress = "marketing@eathello.com",
                Country = "India"
            };
            organizations.Add(organization);

            //Add a business with paint in the name of type restaurant.
            organization = new Organization
            {
                AddressLine1 = "line 1",
                AddressLine2 = "line 2",
                BusinessName = String.Format("India Paints"),
                BusinessType = BusinessType.Supplies,
                EmailAddress = "marketing@indiapaints.com",
                Country = "India"
            };
            organizations.Add(organization);

            organizationCrud.Create(organizations);
        }
    }
}
