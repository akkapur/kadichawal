using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Mvc;
using Interfaces.DataContracts;
using MongoDB.Bson;

namespace IndiTownUI.Controllers
{
    public class SearchController : Controller
    {
        //
        // GET: /Search/

        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            SearchServiceReference.ISearchService searchService = new SearchServiceReference.SearchServiceClient();
            string searchString = collection["searchstring"];
            SearchServiceReference.BusinessType businessType = (SearchServiceReference.BusinessType)Enum.Parse(typeof(SearchServiceReference.BusinessType), collection["category"]);
            SearchServiceReference.SearchResult[] results = searchService.SearchBusiness(searchString, businessType, true);
            List<Interfaces.DataContracts.SearchResult> searchResults =
                results.Select(result => new Interfaces.DataContracts.Organization
                {
                    UserId = result.Organization.UserId,
                    AddressLine1 = result.Organization.AddressLine1,
                    AddressLine2 = result.Organization.AddressLine2,
                    BusinessHours = result.Organization.BusinessHours,
                    BusinessType = (Interfaces.DataContracts.BusinessType)(int)result.Organization.BusinessType,
                    BusinessName = result.Organization.BusinessName,
                    City = result.Organization.City,
                    Country = result.Organization.Country,
                    EmailAddress = result.Organization.EmailAddress,
                    State = result.Organization.State
                }).Select(organization => new SearchResult {Organization = organization}).ToList();

            return View(searchResults);
        }
        
    }
}
