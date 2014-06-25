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
    [Cmdlet(VerbsCommon.Add, "DemoProducts")]
    public class AddProduct : PSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public string Name { get; set; }

        [Parameter(Mandatory = true, Position = 1)]
        public decimal Price { get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            if (Registry.Entities == null)
            {
                WriteWarning("Please use Connect-DemoConnection first!");
                return;
            }

            Product p = new Product()
            {
                Name = Name,
                Price = Price
            };

            Registry.Entities.Products.Add(p);
            Registry.Entities.SaveChanges();

            WriteObject(Mapper.Map<ProductPSM>(p));
        }
        
    }
}
