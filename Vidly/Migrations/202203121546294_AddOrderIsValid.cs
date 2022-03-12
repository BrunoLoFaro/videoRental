namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrderIsValid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "IsValid", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "IsValid");
        }
    }
}
