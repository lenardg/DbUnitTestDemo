using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DbUnitTestDemo;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class DirectEntityAccessTests : SqlCompectBasedTest
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
        public void Test_AddPerson()
        {
            Assert.AreEqual(1, Entities.Persons.Count());
            Person p = Entities.Persons.Single();

            Person person2 = CreateTigerTester();

            Entities.Persons.Add(person2);
            Entities.SaveChanges();

            Assert.AreEqual(2, Entities.Persons.Count());
        }

        [TestMethod]
        public void Test_AddMultiplePersons()
        {
            Assert.AreEqual(1, Entities.Persons.Count());

            Person p = Entities.Persons.Single();
            Assert.AreEqual("Thomas Tester", p.Name);

            Person person2 = CreateTigerTester();
            Person person3 = CreateTonyTester();

            Entities.Persons.Add(person2);
            Entities.Persons.Add(person3);
            Entities.SaveChanges();

            Assert.AreEqual(3, Entities.Persons.Count());
        }

        [TestMethod]
        public void Test_AddProduct()
        {
            Product p = new Product() { Name = "Raspberry icecream", Price = 1.99m };
            Entities.Products.Add(p);
            Entities.SaveChanges();

            var cheapThings = Entities.Products.Where(product => product.Price < 10.0m);
            Assert.AreEqual(1, cheapThings.Count());
        }

        [TestMethod]
        public void Test_NewOrder()
        {
            Order o = new Order();
            o.Who = Entities.Persons.Single();
            o.When = DateTime.UtcNow;
            o.What = new List<OrderLine>(
                new OrderLine[] {
                    new OrderLine(){
                        Product = Entities.Products.Single(p=>p.Price > 500m),
                        Quantity = 1,
                    },
                    new OrderLine(){
                        Product = Entities.Products.Single(p=>p.Price < 500m),
                        Quantity = 3,
                    }   
                });
            Entities.Orders.Add(o);
            Entities.SaveChanges();

            Order newOrder = Entities.Orders.FirstOrDefault();
            Assert.IsNotNull(newOrder);
            Assert.AreEqual(2, newOrder.What.Count());
        }


        private static Person CreateTigerTester()
        {
            Person person2 = new Person()
            {
                Name = "Tiger Tester",
                Address = new Address()
                {
                    Street = "Test avenue 176",
                    Postcode = "00000",
                    City = "Helsinki"
                }
            };
            return person2;
        }

        private static Person CreateTonyTester()
        {
            Person person2 = new Person()
            {
                Name = "Tony Tester",
                Address = new Address()
                {
                    Street = "Bug road 980",
                    Postcode = "00000",
                    City = "Porvoo"
                }
            };
            return person2;
        }

    }
}
