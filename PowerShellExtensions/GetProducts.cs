using AutoMapper;
using DbUnitTestDemo;
using PowerShellExtensions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace PowerShellExtensions
{
    [Cmdlet(VerbsCommon.Get, "DemoProducts")]
    public class GetProducts : PSCmdlet
    {
        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            if ( Registry.Entities == null )
            {
                WriteWarning("Please use Connect-DemoConnection first!");
                return;
            }

            IProductRepository repo = new ProductRepository(Registry.Entities);

            var products = repo.GetProducts();

            foreach (var prod in products.ToList())
            {
                WriteObject(Mapper.Map<ProductPSM>( prod ));
            }
        }
    }
}
