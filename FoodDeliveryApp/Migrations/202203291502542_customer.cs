using System;
using System.Data.Entity.Migrations;
namespace FoodDeliveryApp.Migrations
{

    public partial class customer : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CustomerOreders", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.CustomerOreders", "OrderId", "dbo.Orders");
            DropIndex("dbo.CustomerOreders", new[] { "CustomerId" });
            DropIndex("dbo.CustomerOreders", new[] { "OrderId" });
            AddColumn("dbo.Orders", "CustomerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "CustomerId");
            AddForeignKey("dbo.Orders", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
            DropTable("dbo.CustomerOreders");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CustomerOreders",
                c => new
                    {
                        CustomerId = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                        Quntity = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.CustomerId, t.OrderId });
            
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            DropColumn("dbo.Orders", "CustomerId");
            CreateIndex("dbo.CustomerOreders", "OrderId");
            CreateIndex("dbo.CustomerOreders", "CustomerId");
            AddForeignKey("dbo.CustomerOreders", "OrderId", "dbo.Orders", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CustomerOreders", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
        }
    }
}
