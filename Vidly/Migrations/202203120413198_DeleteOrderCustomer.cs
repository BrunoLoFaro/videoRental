namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteOrderCustomer : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Orders", new[] { "Customer_Id" });
            DropColumn("dbo.Orders", "Customer_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Customer_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "Customer_Id");
            AddForeignKey("dbo.Orders", "Customer_Id", "dbo.Customers", "Id", cascadeDelete: true);
        }
    }
}
