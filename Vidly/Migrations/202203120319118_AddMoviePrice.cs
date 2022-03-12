namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMoviePrice : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "Card_Id", "dbo.Cards");
            DropIndex("dbo.Orders", new[] { "Card_Id" });
            AddColumn("dbo.Movies", "Price", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "CardId", c => c.Byte(nullable: false));
            DropColumn("dbo.Movies", "NumberAvailable");
            DropColumn("dbo.Orders", "Card_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Card_Id", c => c.Int());
            AddColumn("dbo.Movies", "NumberAvailable", c => c.Byte(nullable: false));
            DropColumn("dbo.Orders", "CardId");
            DropColumn("dbo.Movies", "Price");
            CreateIndex("dbo.Orders", "Card_Id");
            AddForeignKey("dbo.Orders", "Card_Id", "dbo.Cards", "Id");
        }
    }
}
