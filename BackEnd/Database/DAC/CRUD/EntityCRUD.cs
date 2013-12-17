
using Database.Configuration;
using Database.Model.Mongo;
using Interfaces.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver.Linq;

namespace Database.DAC.CRUD
{
    public class EntityCRUD<T> : BaseCRUD<T>, ICRUDInitializer
    {
        private MongoWrapper _mongoWrapper;
        private string _collectionName;

        public EntityCRUD()
            : base()
        {
        }

        public EntityCRUD(Type reference)
			: base(reference)
		{
            _collectionName = GetCollectionName(reference);
		}

        public virtual void Initialize()
        {
            _mongoWrapper = new MongoWrapper();
            _mongoWrapper.Initialize(ConfigurationFactory.GetConnectionInfo());
        }

        public override MongoDatabase GetDatabase()
        {
            if (_mongoWrapper != null)
            {
                return _mongoWrapper.GetDatabase();
            }
            else
                throw new ArgumentException("EntityCRUD not Initialized.");
        }

        public override IQueryable<T> GetQueryableCollection()
        {
            return GetDatabase().GetCollection<T>(_collectionName).AsQueryable<T>(); 
        }

        public override MongoCollection<T> GetMongoCollection()
        {
            return GetDatabase().GetCollection<T>(_collectionName);
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
