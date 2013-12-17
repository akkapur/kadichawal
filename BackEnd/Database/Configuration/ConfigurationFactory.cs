using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Configuration
{
    public class ConfigurationFactory
    {
        private static IConnectionInfo _connectionInfo;

        public static IConnectionInfo GetConnectionInfo()
        {
            if (_connectionInfo == null)
            {
                _connectionInfo = new ConnectionInfo();

                if(ConfigurationManager.AppSettings["DatabaseType"].Equals("MongoDB", StringComparison.OrdinalIgnoreCase))
                    _connectionInfo.DatabaseType = DatabaseType.MongoDB;
                else
                    throw new ArgumentException("Unknown or No DatabaseType mentioned in the config file.", "DatabaseType");

                if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["DatabaseName"]))
                    _connectionInfo.DatabaseName = ConfigurationManager.AppSettings["DatabaseName"];
                else
                    throw new ArgumentException("No Database Name Provided.", "DatabaseName");

                if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["Host"]))
                    _connectionInfo.Host = ConfigurationManager.AppSettings["Host"];
                else
                    throw new ArgumentException("No Host Provided.", "Host");

                if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["Port"]))
                    _connectionInfo.Port = Int32.Parse(ConfigurationManager.AppSettings["Port"]);
                else
                {
                    switch (_connectionInfo.DatabaseType)
                    {
                        case DatabaseType.MongoDB:
                            _connectionInfo.Port = 27017;
                            break;
                        default:
                            throw new ArgumentException("Unknown Database Type.");
                    }
                }

                switch (_connectionInfo.DatabaseType)
                {
                    case DatabaseType.MongoDB:
                        _connectionInfo.ConnectionString = String.Format("mongodb://{0}", _connectionInfo.Host);
                        break;
                    default:
                        break;
                }
            }

            return _connectionInfo;
        }
    }
}
