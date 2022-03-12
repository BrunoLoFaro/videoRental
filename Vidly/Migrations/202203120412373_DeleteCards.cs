namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteCards : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Cards");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Cards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        Brand = c.String(),
                        Code = c.Byte(nullable: false),
                        OwnerFullName = c.String(),
                        ExpirationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
