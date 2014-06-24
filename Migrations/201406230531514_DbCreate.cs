namespace DbUnitTestDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Long(nullable: false, identity: true),
                        When = c.DateTime(nullable: false),
                        Who_PersonId = c.Long(),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.People", t => t.Who_PersonId)
                .Index(t => t.Who_PersonId);
            
            CreateTable(
                "dbo.OrderLines",
                c => new
                    {
                        OrderLineId = c.Long(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        Order_OrderId = c.Long(),
                        Product_ProductId = c.Long(),
                    })
                .PrimaryKey(t => t.OrderLineId)
                .ForeignKey("dbo.Orders", t => t.Order_OrderId)
                .ForeignKey("dbo.Products", t => t.Product_ProductId)
                .Index(t => t.Order_OrderId)
                .Index(t => t.Product_ProductId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ProductId);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        PersonId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Address_AddressId = c.Int(),
                    })
                .PrimaryKey(t => t.PersonId)
                .ForeignKey("dbo.Addresses", t => t.Address_AddressId)
                .Index(t => t.Address_AddressId);
            
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressId = c.Int(nullable: false, identity: true),
                        Street = c.String(),
                        City = c.String(),
                        Postcode = c.String(),
                        Country = c.String(),
                    })
                .PrimaryKey(t => t.AddressId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "Who_PersonId", "dbo.People");
            DropForeignKey("dbo.People", "Address_AddressId", "dbo.Addresses");
            DropForeignKey("dbo.OrderLines", "Product_ProductId", "dbo.Products");
            DropForeignKey("dbo.OrderLines", "Order_OrderId", "dbo.Orders");
            DropIndex("dbo.People", new[] { "Address_AddressId" });
            DropIndex("dbo.OrderLines", new[] { "Product_ProductId" });
            DropIndex("dbo.OrderLines", new[] { "Order_OrderId" });
            DropIndex("dbo.Orders", new[] { "Who_PersonId" });
            DropTable("dbo.Addresses");
            DropTable("dbo.People");
            DropTable("dbo.Products");
            DropTable("dbo.OrderLines");
            DropTable("dbo.Orders");
        }
    }
}
