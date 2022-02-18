namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'01948997-7dfa-4ec1-a24c-b2fc4e49ab3e', N'admin@vidly.com', 0, N'AMeT1yePM6dqE+8YWcPviu7ub6nNfJZ5BnwWK4fleLX9hy5d8HZZiIhfYG76XIU6BA==', N'abb1ac90-1aa9-4f43-af27-5941c24240a3', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'506be9b5-26fe-49f6-a0f6-488726e411f9', N'CanManageMovies')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'01948997-7dfa-4ec1-a24c-b2fc4e49ab3e', N'506be9b5-26fe-49f6-a0f6-488726e411f9')
");
        }

        public override void Down()
        {
        }
    }
}
