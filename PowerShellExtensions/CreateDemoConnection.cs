using AutoMapper;
using DbUnitTestDemo;
using PowerShellExtensions.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace PowerShellExtensions
{
    [Cmdlet(VerbsCommunications.Connect, "DemoConnection")]
    public class CreateDemoConnection : Cmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public string Server { get; set; }

        [Parameter(Mandatory = true, Position = 1)]
        public string Database { get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            EnsureAutomapper();

            if ( Registry.Entities != null )
            {
                WriteWarning("Already initialized!");
                return;
            }

            string connectionString = string.Format("MultipleActiveResultSets=true;Data source={0};Initial catalog={1};Integrated Security=true",
                Server, Database);

            var conn = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateConnection();
            conn.ConnectionString = connectionString;
            conn.Open();

            Registry.Entities = new DbUnitTestDemo.Entities(conn);

            WriteVerbose("Connected.");

        }

        private void EnsureAutomapper()
        {
            if ( Mapper.FindTypeMapFor<Product,ProductPSM>() == null )
            {
                Mapper.CreateMap<Product, ProductPSM>();
            }
        }
    }
}
