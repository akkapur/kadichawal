using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Database.Configuration;

namespace Database.Model.Mongo
{
    public interface IMongoWrapper
    {
        void Initialize(IConnectionInfo connectionInfo);

        /// <summary>
        /// Provides a reference to the database instance on a mongo server.
        /// </summary>
        /// <returns></returns>
        MongoDatabase GetDatabase();
        bool DatabaseExists();
        void RemoveDatabase();
    }

    public class MongoWrapper : IMongoWrapper
    {
        private static MongoClient _client;
        private static MongoDatabase _database;
        private static IConnectionInfo _connectionInfo;
        

        public void Initialize(IConnectionInfo connectionInfo)
        {
            _connectionInfo = connectionInfo;
            _client = new MongoClient(connectionInfo.ConnectionString);
        }

        public MongoDatabase GetDatabase()
        {
            if (_database == null)
            {
                MongoServer mongoServer = _client.GetServer();
                _database = mongoServer.GetDatabase(_connectionInfo.DatabaseName);
            }
            return _database;
        }

        public bool DatabaseExists()
        {
            MongoServer mongoServer = _client.GetServer();
            return mongoServer.DatabaseExists(_connectionInfo.DatabaseName);
        }

        public void RemoveDatabase()
        {
            MongoServer mongoServer = _client.GetServer();
            mongoServer.DropDatabase(_connectionInfo.DatabaseName);
        }
    }
}
