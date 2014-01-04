using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Mvc;
using Antlr.Runtime;
using IndiTownUI.ReviewServiceReference;
using IndiTownUI.SearchServiceReference;
using MongoDB.Bson;
using Review = IndiTownUI.SearchServiceReference.Review;
using SearchResult = Interfaces.DataContracts.SearchResult;

namespace IndiTownUI.Controllers
{
    public class SearchController : Controller
    {
        //
        // GET: /Search/

        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            string searchString = collection["searchstring"];
            SearchServiceReference.BusinessType businessType = (SearchServiceReference.BusinessType)Enum.Parse(typeof(SearchServiceReference.BusinessType), collection["category"]);

            IEnumerable<SearchServiceReference.SearchResult> results = String.IsNullOrWhiteSpace(searchString)
                ? GetSearchResultsByCategory(businessType)
                : GetSearchResultsForSearchTerm(searchString, businessType);

            List<Interfaces.DataContracts.SearchResult> searchResults = new List<SearchResult>();

            foreach (SearchServiceReference.SearchResult result in results)
            {
                Interfaces.DataContracts.Organization organization = new Interfaces.DataContracts.Organization
                {
                    UserId = result.Organization.UserId,
                    AddressLine1 = result.Organization.AddressLine1,
                    AddressLine2 = result.Organization.AddressLine2,
                    BusinessHours = result.Organization.BusinessHours,
                    BusinessType = (Interfaces.DataContracts.BusinessType) (int) result.Organization.BusinessType,
                    BusinessName = result.Organization.BusinessName,
                    City = result.Organization.City,
                    Country = result.Organization.Country,
                    EmailAddress = result.Organization.EmailAddress,
                    State = result.Organization.State,
                    OrganizationId = result.Organization.OrganizationId
                };

                IndiTownUI.ReviewServiceReference.ReviewServiceClient reviewServiceClient = new ReviewServiceClient();
                IEnumerable<ReviewServiceReference.Review> reviews =
                    reviewServiceClient.GetBusinessReview(organization.OrganizationId);

                List<Interfaces.DataContracts.Review> searchResultReviews = new List<Interfaces.DataContracts.Review>();

                foreach (ReviewServiceReference.Review review in reviews)
                {
                    Interfaces.DataContracts.Review tempReview = new Interfaces.DataContracts.Review
                    {
                        ReviewId = review.ReviewId,
                        ReviewerId = review.ReviewerId,
                        ReviewText = review.ReviewText,
                        OrganizationId = review.OrganizationId
                    };
                    searchResultReviews.Add(tempReview);
                }

                Interfaces.DataContracts.SearchResult searchResult = new Interfaces.DataContracts.SearchResult
                {
                    Organization = organization,
                    Reviews = searchResultReviews.ToArray()
                };

                searchResults.Add(searchResult);
            }

            return View(searchResults);
        }

        private IEnumerable<SearchServiceReference.SearchResult> GetSearchResultsForSearchTerm(string searchString, SearchServiceReference.BusinessType businessType)
        {
            SearchServiceReference.ISearchService searchService = new SearchServiceReference.SearchServiceClient();
            SearchServiceReference.SearchResult[] results = searchService.SearchBusiness(searchString, businessType, true);
            return results;
        }

        private IEnumerable<SearchServiceReference.SearchResult> GetSearchResultsByCategory(SearchServiceReference.BusinessType businessType)
        {
            IndiTownUI.OrganizationServiceReference.OrganizationServiceClient organizationClient = new OrganizationServiceReference.OrganizationServiceClient();
            IEnumerable<OrganizationServiceReference.Organization> organizations = organizationClient.GetOrganizationByType((OrganizationServiceReference.BusinessType) (int) businessType);
            List<SearchServiceReference.SearchResult> searchResults = new List<SearchServiceReference.SearchResult>();

            foreach (OrganizationServiceReference.Organization organization in organizations)
            {
                SearchServiceReference.Organization tempOrganization = new Organization
                {
                    UserId = organization.UserId,
                    AddressLine1 = organization.AddressLine1,
                    AddressLine2 = organization.AddressLine2,
                    BusinessHours = organization.BusinessHours,
                    BusinessType = (SearchServiceReference.BusinessType) (int) organization.BusinessType,
                    BusinessName = organization.BusinessName,
                    City = organization.City,
                    Country = organization.Country,
                    EmailAddress = organization.EmailAddress,
                    State = organization.State,
                    OrganizationId = organization.OrganizationId
                };

                IndiTownUI.ReviewServiceReference.ReviewServiceClient reviewServiceClient = new ReviewServiceClient();
                IEnumerable<ReviewServiceReference.Review> reviews =
                    reviewServiceClient.GetBusinessReview(organization.OrganizationId);

                List<SearchServiceReference.Review> searchResultReviews = new List<Review>();

                foreach (ReviewServiceReference.Review review in reviews)
                {
                    SearchServiceReference.Review tempReview = new Review
                    {
                        ReviewId = review.ReviewId,
                        ReviewerId = review.ReviewerId,
                        ReviewText = review.ReviewText,
                        OrganizationId = review.OrganizationId
                    };
                    searchResultReviews.Add(tempReview);
                }

                SearchServiceReference.SearchResult searchResult = new SearchServiceReference.SearchResult
                {
                    Organization = tempOrganization,
                    Reviews = searchResultReviews.ToArray()
                };

                searchResults.Add(searchResult);
            }

            return searchResults;
        }
        
    }
}
