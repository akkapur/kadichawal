using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Database.DAC.Search;
using Interfaces.DataContracts;
using Interfaces.ServiceContracts;

namespace IndiTownServices.services
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SearchService" in code, svc and config file together.
	// NOTE: In order to launch WCF Test Client for testing this service, please select SearchService.svc or SearchService.svc.cs at the Solution Explorer and start debugging.
	public class SearchService : ISearchService
	{
        public IEnumerable<SearchResult> SearchBusiness(string searchString, BusinessType businessType = BusinessType.Unknown, bool isFullTextSearch = true)
	    {
	        ISearch search = new Search();
	        IEnumerable<Organization> organizations = search.DoSearch<Organization>(searchString, isFullTextSearch, businessType);
            List<SearchResult> searchResults = organizations.Select(organization => new SearchResult {Organization = organization}).ToList();
            return searchResults;
	    }
	}
}
