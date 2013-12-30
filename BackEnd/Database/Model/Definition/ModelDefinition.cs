
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
    public class CollectionModel
    {
        public CollectionModel()
        {
            TextIndexedPropertyNames = new List<string>();
        }

        public string CollectionName { get; set; }
        public List<string> TextIndexedPropertyNames { get; set; }
    }

    public interface IModelDefinition
    {
        void SetupModel();
    }

    /// <summary>
    /// Code to setup the data model. Basically creating collections and indices.
    /// </summary>
    public class ModelDefinition : IModelDefinition
    {
        private List<CollectionModel> collectionModels = new List<CollectionModel>();

        //creating empty collections and indices.
        public void SetupModel()
        {
            BuildModelConfig();

            foreach (CollectionModel collectionModel in collectionModels)
            {
                IMongoWrapper wrapper = new MongoWrapper();
                wrapper.Initialize(ConfigurationFactory.GetConnectionInfo());
                MongoDatabase database = wrapper.GetDatabase();
                if (!database.CollectionExists(collectionModel.CollectionName))
                {             
                    database.CreateCollection(collectionModel.CollectionName, CollectionOptions.SetAutoIndexId(true));
                }

                MongoCollection collection = database.GetCollection(collectionModel.CollectionName);
                foreach(string propertyName in collectionModel.TextIndexedPropertyNames)
                {
                    collection.EnsureIndex(new IndexKeysDocument(propertyName, "text"));
                }
            }
        }

        private void BuildModelConfig()
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
                                CollectionModel collectionModel = new CollectionModel();
                                collectionModel.CollectionName = myAttribute.CollectionName;

                                foreach (PropertyInfo prop in type.GetProperties())
                                {
                                    if (prop.GetCustomAttributes(typeof(IsTextIndexedAttribute)).Any())
                                    {
                                        collectionModel.TextIndexedPropertyNames.Add(prop.Name);
                                    }
                                }
                                collectionModels.Add(collectionModel);
                            }
                            else
                                throw new ArgumentException(String.Format("No CollectionAttribute attribute defined for {0}", type.ToString()));
                        }
                    }
                }
            }
            throw new InvalidProgramException("Could not find the Database assembly.");
        }
    }
}
