namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeOrderCardToCardId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "Card_Id", "dbo.Cards");
            DropIndex("dbo.Orders", new[] { "Card_Id" });
            AddColumn("dbo.Orders", "CardId", c => c.Byte(nullable: false));
            DropColumn("dbo.Orders", "Card_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Card_Id", c => c.Int());
            DropColumn("dbo.Orders", "CardId");
            CreateIndex("dbo.Orders", "Card_Id");
            AddForeignKey("dbo.Orders", "Card_Id", "dbo.Cards", "Id");
        }
    }
}
