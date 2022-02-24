namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers1 : DbMigration
    {
        public override void Up()
        {
            Sql(
                @"INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [DrivingLicense], [TelephoneNumber]) VALUES (N'96ce3215-e849-41ae-8dcc-b6ff840898bd', N'admin@vidly.com', 0, N'AJc+j9AUPe1lA6/NU4P87N9vD9Q9Y3xaRZqhJGaoub4IgRdrs6Czf8PZvoN06sqLsA==', N'12b8364a-f191-48fd-9a78-96f0976a122e', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com', N'adminLicense', N'1155643122')
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'acec2039-c91d-4e5a-9008-035b939991be', N'CanManageCustomers')
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'506be9b5-26fe-49f6-a0f6-488726e411f9', N'CanManageMovies')
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'96ce3215-e849-41ae-8dcc-b6ff840898bd', N'506be9b5-26fe-49f6-a0f6-488726e411f9')
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'96ce3215-e849-41ae-8dcc-b6ff840898bd', N'acec2039-c91d-4e5a-9008-035b939991be')
                ");
        }
        
        public override void Down()
        {
        }
    }
}
