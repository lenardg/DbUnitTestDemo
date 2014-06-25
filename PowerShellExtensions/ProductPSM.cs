using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerShellExtensions
{
    public class ProductPSM
    {
        public virtual long ProductId { get; set; }

        public virtual string Name { get; set; }

        public virtual decimal Price { get; set; }
    }
}
