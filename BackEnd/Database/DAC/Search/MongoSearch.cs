using Database.Configuration;
using Database.Model.Mongo;
using Interfaces.Attributes;
using Interfaces.DataContracts;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.DAC.Search
{

    public class TextSearchResult<T>
    {
       public T obj { get; set; }
       public double score { get; set; }
    }

    public interface IMongoSearch
    {
        /// <summary>
        /// Finds a business based on the search string and the business type. This does not support wild card searches. 
        /// </summary>
        /// <param name="search">search string entered by the user.</param>
        /// <param name="businessType">type of business to search for. Searching across all by default.</param>
        IEnumerable<T> Search<T>(string search, BusinessType businessType = BusinessType.Unknown)
            where T : class, new();
    }

    /// <summary>
    /// A dedicated class that uses MongoDB search API.
    /// </summary>
    public class MongoSearch : IMongoSearch
    {
        private MongoWrapper _mongoWrapper;

        public IEnumerable<T> Search<T>(string search, BusinessType businessType)
            where T : class, new()
        {
            if(String.IsNullOrWhiteSpace(search))
                throw new InvalidOperationException("No Search term provided for full text searching.");

            _mongoWrapper = new MongoWrapper();
            _mongoWrapper.Initialize(ConfigurationFactory.GetConnectionInfo());

            CommandDocument textSearchCommand = null;

            if (businessType == BusinessType.Unknown)
            {
                textSearchCommand = new CommandDocument
                {

                    {"text", typeof (T).Name},
                    {"search", search}
                };
            }
            else
            {
                textSearchCommand = new CommandDocument
                {

                    {"text", typeof (T).Name},
                    {"search", search},
                    {"filter", new BsonDocument {{"BusinessType", businessType}}}
                };
            }
            var commandResult = _mongoWrapper.GetDatabase().RunCommand(textSearchCommand);
            if (!commandResult.Ok) return null;

            IEnumerable<BsonDocument> results = commandResult.Response["results"].AsBsonArray.Select(x => x.AsBsonDocument);
            var resultObjects = results.Select(item => item.AsBsonDocument);
            return resultObjects.Select(BsonSerializer.Deserialize<TextSearchResult<T>>).OrderByDescending(x => x.score).Select(x => x.obj);
        }

        ///// <summary>
        ///// Finds a business based on the search string and the business type. 
        ///// TODO : Add support for business type in the query.
        ///// </summary>
        ///// <param name="searchString"></param>
        ///// <param name="businessType"></param>
        //public void SearchBusiness(string searchString, BusinessType businessType = BusinessType.Unknown)
        //{
        //    _mongoWrapper = new MongoWrapper();
        //    _mongoWrapper.Initialize(ConfigurationFactory.GetConnectionInfo());

        //    MongoCollection<Organization> collection = GetMongoCollection();
        //    var textSearchCommand = new CommandDocument
        //        {
        //            { "text", collection.Name },
        //            { "search", searchString }
        //        };
        //    var commandResult = _mongoWrapper.GetDatabase().RunCommand(textSearchCommand);
        //    var response = commandResult.Response;
        //    foreach (BsonDocument result in response["results"].AsBsonArray)
        //    {
        //        // process result
        //        Organization organization = BsonSerializer.Deserialize<Organization>(result);
        //        Console.WriteLine(organization.ToString());
        //    }
        //}

        private MongoCollection<Organization> GetMongoCollection()
        {
            string collectionName = GetCollectionName(typeof(Organization));
            return _mongoWrapper.GetDatabase().GetCollection<Organization>(collectionName);
        }

        private string GetCollectionName(Type t)
        {
            CollectionAttribute myAttribute =
            (CollectionAttribute)Attribute.GetCustomAttribute(t, typeof(CollectionAttribute));

            if (myAttribute != null)
            {
                return myAttribute.CollectionName;
            }
            else
                throw new ArgumentException(String.Format("No CollectionAttribute attribute defined for {0}", t.ToString()));
        }

    }
}
