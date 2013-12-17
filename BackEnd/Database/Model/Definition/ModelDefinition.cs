
using Database.Configuration;
using Database.Model.Mongo;
using Interfaces.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Database.Model.Definition
{
    public interface IModelDefinition
    {
        void SetupModel();
    }

    public class ModelDefinition : IModelDefinition
    {
        public void SetupModel()
        {
            foreach (string collectionName in GetCollectionNames())
            {
                IMongoWrapper wrapper = new MongoWrapper();
                wrapper.Initialize(ConfigurationFactory.GetConnectionInfo());
                MongoDatabase database = wrapper.GetDatabase();
                if (!database.CollectionExists(collectionName))
                {
                    database.CreateCollection(collectionName, CollectionOptions.SetAutoIndexId(true));
                }
            }
        }

        private IEnumerable<string> GetCollectionNames()
        {
            Assembly assembly = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.Equals("Database", StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (assembly != null)
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.GetCustomAttributes(typeof(CollectionAttribute), true).Length > 0)
                    {
                        {
                            CollectionAttribute myAttribute = (CollectionAttribute)Attribute.GetCustomAttribute(type, typeof(CollectionAttribute));
                            if (myAttribute != null)
                            {
                                yield return myAttribute.CollectionName;
                            }
                            else
                                throw new ArgumentException(String.Format("No CollectionAttribute attribute defined for {0}", type.ToString()));
                        }
                    }
                }
            }
            throw new InvalidProgramException("Could not find the Datbase assembly.");
        }
    }
}
