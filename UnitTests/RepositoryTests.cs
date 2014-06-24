using DbUnitTestDemo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class RepositoryTests : SqlCompectBasedTest
    {
        protected override void Seed()
        {
            base.Seed();

            Person person1 = new Person()
            {
                Name = "Thomas Tester",
                Address = new Address()
                {
                    Street = "Test avenue 176",
                    Postcode = "00000",
                    City = "Helsinki"
                }
            };

            Product p1 = new Product()
            {
                Name = "Notebook",
                Price = 999.9m,
            };

            Product p2 = new Product()
            {
                Name = "Smartphone",
                Price = 199.9m,
            };

            Entities.Persons.Add(person1);

            Entities.Products.Add(p1);
            Entities.Products.Add(p2);

            Entities.SaveChanges();
        }

        [TestMethod]
        public void ProductRepository_GetProducts()
        {
            IProductRepository repo = new ProductRepository(this.Entities);

            var products = repo.GetProducts();

            Assert.AreEqual(2, products.Count());
        }

        [TestMethod]
        public void ProductController_ShowProducts()
        {
            IProductRepository repo = new ProductRepository(this.Entities);
            ProductController controller = new ProductController(repo);

            var product = controller.ShowProduct(1);

            Assert.IsNotNull(product);
        }
    }
}
