using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbUnitTestDemo
{
    public class ProductController
    {
        private readonly IProductRepository repo;
        public ProductController ( IProductRepository repo )
        {
            this.repo = repo;
        }
        public Product ShowProduct( long id )
        {
            return repo.GetProducts().SingleOrDefault(p=>p.ProductId == id);
        }
    }
}
