
using Interfaces.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Database.DAC.CRUD
{
    public interface ICRUDInitializer
    {
        void Initialize();
    }

    public interface IBaseCRUD<T>
    {
        void Create(T item);
        void Create(IList<T> items);
        IQueryable<T> ReadAll();
        T Read(string id);
        //IQueryable<Guid> ReadIds(Expression<Func<T, bool>> criteria);
        IQueryable<T> Read(Expression<Func<T, bool>> criteria);
        void Update(T item);
        void Delete(string id);
        void Delete(Expression<Func<T, bool>> criteria);
    }

    public abstract class BaseCRUD<T> : IBaseCRUD<T>
    {
        protected BaseCRUD()
			: this(typeof(T))
		{
		}

        protected BaseCRUD(Type reference)
        {
        }

        public abstract MongoDatabase GetDatabase();
        public abstract IQueryable<T> GetQueryableCollection();
        public abstract MongoCollection<T> GetMongoCollection();

        public void Create(T item)
        {
            MongoCollection<T> collection = GetMongoCollection();
            collection.Insert<T>(item);
        }

        public void Create(IList<T> items)
        {
            MongoCollection<T> collection = GetMongoCollection();
            collection.InsertBatch<T>(items);
        }

        public IQueryable<T> ReadAll()
        {
            return GetQueryableCollection();
        }

        public IQueryable<T> Read(Expression<Func<T, bool>> criteria)
        {
            IQueryable<T> collection = GetQueryableCollection();
            return collection.Where<T>(criteria);
        }

        public T Read(string id)
        {
            MongoCollection<T> collection = GetMongoCollection();
            return collection.FindOneAs<T>(Query.EQ("_id", id));
        }

        public void Update(T item)
        {
            MongoCollection<T> collection = GetMongoCollection();
            collection.Save<T>(item);
        }

        public void Delete(string id)
        {
            MongoCollection<T> collection = GetMongoCollection();
            collection.Remove(Query.EQ("_id", id));
        }

        public void Delete(Expression<Func<T, bool>> criteria)
        {
            IQueryable<T> items = Read(criteria);
            foreach (T item in items)
            {
                foreach (PropertyInfo prop in item.GetType().GetProperties())
                {
                    if(prop.GetCustomAttributes(typeof(PrimaryKeyAttribute)).Any())
                    {
                        String value = prop.GetValue(item, null).ToString();
                        Delete(value);
                    }
                }
            }
        }
    }
}
