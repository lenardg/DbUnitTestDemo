using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbUnitTestDemo
{
    public class Entities : DbContext
    {
        public Entities ()
        {
        }
        public Entities(DbConnection connection)
            : base(connection, true)
        {
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

    public class Person
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public virtual long PersonId { get; set; }

        public virtual string Name { get; set; }

        public virtual Address Address { get; set; }
    }

    public class Product
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public virtual long ProductId { get; set; }

        public virtual string Name { get; set; }

        public virtual decimal Price { get; set; }

    }

    public class Order
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public virtual long OrderId { get; set; }

        public virtual DateTime When { get; set; }

        public virtual IList<OrderLine> What { get; set; }

        public virtual Person Who { get; set; }
    }

    public class OrderLine
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public virtual long OrderLineId { get; set; }

        public virtual int Quantity { get; set; }

        public virtual Product Product { get; set; }

        public virtual Order Order { get; set; }
    }

    public class Address
    {
        public virtual int AddressId { get; set; }

        public virtual string Street { get; set; }

        public virtual string City { get; set; }

        public virtual string Postcode { get; set; }

        public virtual string Country { get; set; }
    }
}
