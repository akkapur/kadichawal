using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces.DataContracts;

namespace Database.DAC.Search
{
    public interface ISearch
    {
        /// <summary>
        /// Performs searching across the corpus based on user search query.
        /// </summary>
        /// <typeparam name="T">Type of Result.</typeparam>
        /// <param name="search">Search string.</param>
        /// <param name="businessType">Type of business to perform faceted search.</param>
        /// <param name="isFullText">Perform full text search.</param>
        /// <returns></returns>
        IEnumerable<T> DoSearch<T>(string search, bool isFullText, BusinessType businessType = BusinessType.Unknown)
            where T : class, new();
    }

    public class Search : ISearch
    {
        public IEnumerable<T> DoSearch<T>(string search, bool isFullText, BusinessType businessType = BusinessType.Unknown)
            where T : class, new()
        {
            if (isFullText)
            {
                IMongoSearch mongoSearch = new MongoSearch();
                return mongoSearch.Search<T>(search, businessType);
            }

            throw new NotImplementedException("Only Full Text Searching Is allowed.");
        }
    }
}
