namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCardToRental : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rentals", "Card_Id", c => c.Int());
            CreateIndex("dbo.Rentals", "Card_Id");
            AddForeignKey("dbo.Rentals", "Card_Id", "dbo.Cards", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rentals", "Card_Id", "dbo.Cards");
            DropIndex("dbo.Rentals", new[] { "Card_Id" });
            DropColumn("dbo.Rentals", "Card_Id");
        }
    }
}
