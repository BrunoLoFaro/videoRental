namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPropsToAppUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "DrivingLicense", c => c.String(nullable: false, maxLength: 255));
            AddColumn("dbo.AspNetUsers", "TelephoneNumber", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "TelephoneNumber");
            DropColumn("dbo.AspNetUsers", "DrivingLicense");
        }
    }
}
