namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteOrderCardId : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Orders", "CardId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "CardId", c => c.Byte(nullable: false));
        }
    }
}
