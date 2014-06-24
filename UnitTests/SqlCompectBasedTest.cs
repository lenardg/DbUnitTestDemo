using DbUnitTestDemo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.Common;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public abstract class SqlCompectBasedTest
    {
        private Entities entities;

        private string currentFile;

        public Entities Entities
        {
            get { return entities; }
        }
 
        [TestInitialize]
        public void Initialize ()
        {
            currentFile = Path.GetTempFileName();
            string connectionString = string.Format(
                "Data Source={0};Persist Security Info=False", currentFile);

            var conn = DbProviderFactories.GetFactory("System.Data.SqlServerCe.4.0").CreateConnection();
            conn.ConnectionString = connectionString;
            conn.Open();

            entities = new Entities(conn);
            entities.Database.CreateIfNotExists();

            Seed();
        }

        [TestCleanup]
        public void Teardown ()
        {
            entities.Dispose();
            entities = null;

            File.Delete(currentFile);
        }

        protected virtual void Seed()
        { }
    }
}
