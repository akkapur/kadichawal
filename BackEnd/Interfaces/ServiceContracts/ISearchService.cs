using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Interfaces.DataContracts;

namespace Interfaces.ServiceContracts
{
    [ServiceContract]
    public interface ISearchService
    {
        [OperationContract]
        IEnumerable<SearchResult> SearchBusiness(string searchString, BusinessType businessType = BusinessType.Unknown,
            bool isFullTextSearch = true);
    }
}
