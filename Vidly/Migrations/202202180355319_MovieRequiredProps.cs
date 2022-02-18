namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MovieRequiredProps : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "GenreName", c => c.String(nullable: false, maxLength: 255));
            DropColumn("dbo.Movies", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movies", "Name", c => c.String(nullable: false, maxLength: 255));
            DropColumn("dbo.Movies", "GenreName");
        }
    }
}
