namespace FoodDeliveryApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class customerorder : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerOreders",
                c => new
                    {
                        CustomerId = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                        Quntity = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.CustomerId, t.OrderId })
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerOreders", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.CustomerOreders", "CustomerId", "dbo.Customers");
            DropIndex("dbo.CustomerOreders", new[] { "OrderId" });
            DropIndex("dbo.CustomerOreders", new[] { "CustomerId" });
            DropTable("dbo.Customers");
            DropTable("dbo.CustomerOreders");
        }
    }
}
