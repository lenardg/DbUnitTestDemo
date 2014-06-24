using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbUnitTestDemo
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts();
    }

    public class ProductRepository : IProductRepository
    {
        private readonly Entities entities;
        public ProductRepository( Entities entities )
        {
            this.entities = entities;
        }

        public IEnumerable<Product> GetProducts()
        {
            return entities.Products;
        }
    }
}
