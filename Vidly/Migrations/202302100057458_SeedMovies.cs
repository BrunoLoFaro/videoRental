namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedMovies : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                SET IDENTITY_INSERT [dbo].[Movies] ON
                INSERT INTO [dbo].[Movies] ([Id], [Name], [GenreId], [DateAdded], [ReleaseDate], [NumberInStock], [Price], [ImageUrl]) VALUES (01, 'La Momia', 1, 18-02-2000, 03-01-2000, 31, 999, './momia.jpg')
                INSERT INTO [dbo].[Movies] ([Id], [Name], [GenreId], [DateAdded], [ReleaseDate], [NumberInStock], [Price], [ImageUrl]) VALUES (02, 'Gato con Botas', 1, 12-01-1991, 12-01-1990, 31, 242, './Gato.jpg')
                INSERT INTO [dbo].[Movies] ([Id], [Name], [GenreId], [DateAdded], [ReleaseDate], [NumberInStock], [Price], [ImageUrl]) VALUES (03, 'Forest Gump', 1, 04-01-1994, 11-01-1990, 31, 242, './Forest.jpg')
                INSERT INTO [dbo].[Movies] ([Id], [Name], [GenreId], [DateAdded], [ReleaseDate], [NumberInStock], [Price], [ImageUrl]) VALUES (04, 'Interstellar', 1, 03-03-1999, 12-03-1990, 31, 242, './Interstellar.jpg')
                INSERT INTO [dbo].[Movies] ([Id], [Name], [GenreId], [DateAdded], [ReleaseDate], [NumberInStock], [Price], [ImageUrl]) VALUES (05, 'Memento', 1, 11-01-1990, 11-2-1990, 31, 242, './Memento.jpg')
                SET IDENTITY_INSERT [dbo].[Movies] OFF
                ");
            //set id also.
        }
        
        public override void Down()
        {
        }
    }
}
