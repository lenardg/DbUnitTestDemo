using AutoMapper;
using PowerShellExtensions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace PowerShellExtensions
{
    [Cmdlet(VerbsData.Update, "DemoProductPrice")]
    public class UpdateProductPrice : Cmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public ProductPSM Product { get; set; }

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

            var product = Registry.Entities.Products.Single(p => p.ProductId == Product.ProductId);
            product.Price = Price;
            Registry.Entities.SaveChanges();

            WriteObject(Mapper.Map<ProductPSM>(product));
        }
    }
}
