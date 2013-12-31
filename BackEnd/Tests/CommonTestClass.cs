using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Database.Configuration;
using Database.Model.Mongo;
using Database.Model.Definition;

namespace Tests
{
    [TestClass()]
    public sealed class CommonTestClass
    {
        [AssemblyInitialize]
        public static void AssemblySetup(TestContext context)
        {
            SetUpDB();

            //IConnectionInfo connectionInfo = ConfigurationFactory.GetConnectionInfo();
            //IMongoWrapper mongoWrapper = new MongoWrapper();
            //mongoWrapper.Initialize(ConfigurationFactory.GetConnectionInfo());
            //mongoWrapper.GetDatabase();
        }

        [AssemblyCleanup]
        public static void AssemblyTearDown()
        {
            RemoveTestDB();
        }

        private static void SetUpDB()
        {
            IModelDefinition modelDef = new ModelDefinition();
            modelDef.SetupModel();
        }

        private static void RemoveTestDB()
        {
            IConnectionInfo connectionInfo = ConfigurationFactory.GetConnectionInfo();
            IMongoWrapper mongoWrapper = new MongoWrapper();
            mongoWrapper.Initialize(ConfigurationFactory.GetConnectionInfo());
            if (mongoWrapper.DatabaseExists())
                mongoWrapper.RemoveDatabase();
        }
    }
}
