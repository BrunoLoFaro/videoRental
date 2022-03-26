namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderCardIdLong : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "CardId", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "CardId");
        }
    }
}
