namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddItem : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Movie_Id = c.Int(),
                        Order_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Movies", t => t.Movie_Id)
                .ForeignKey("dbo.Orders", t => t.Order_Id)
                .Index(t => t.Movie_Id)
                .Index(t => t.Order_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.Items", "Movie_Id", "dbo.Movies");
            DropIndex("dbo.Items", new[] { "Order_Id" });
            DropIndex("dbo.Items", new[] { "Movie_Id" });
            DropTable("dbo.Items");
        }
    }
}
