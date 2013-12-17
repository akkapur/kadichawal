using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Configuration
{
    public enum DatabaseType
    {
        MongoDB = 0,
    }

    public interface IConnectionInfo
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        DatabaseType DatabaseType { get; set; }
        string Host { get; set; }
        int Port { get; set; }
    }

    public sealed class ConnectionInfo : IConnectionInfo
    {
        private DatabaseType _databaseType = DatabaseType.MongoDB;

        public DatabaseType DatabaseType 
        {
            get { return _databaseType; }
            set { _databaseType = value; }
        }

        public string Host { get; set; }

        public int Port { get; set; }

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }
    }
}
