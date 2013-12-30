using Database.Configuration;
using Database.Model.Mongo;
using Interfaces.Attributes;
using Interfaces.DataContracts;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.DAC.Search
{
    /// <summary>
    /// A dedicated class that used MongoDB search API.
    /// </summary>
    public class MongoSearch
    {
        private MongoWrapper _mongoWrapper;

        public void SearchBusiness(string searchString)
        {
            _mongoWrapper = new MongoWrapper();
            _mongoWrapper.Initialize(ConfigurationFactory.GetConnectionInfo());

            MongoCollection<Organization> collection = GetMongoCollection();
            var textSearchCommand = new CommandDocument
                {
                    { "text", collection.Name },
                    { "search", searchString }
                };
            var commandResult = _mongoWrapper.GetDatabase().RunCommand(textSearchCommand);
            var response = commandResult.Response;
        }

        public MongoCollection<Organization> GetMongoCollection()
        {
            string collectionName = GetCollectionName(typeof(Organization));
            return _mongoWrapper.GetDatabase().GetCollection<Organization>(collectionName);
        }

        public string GetCollectionName(Type t)
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
