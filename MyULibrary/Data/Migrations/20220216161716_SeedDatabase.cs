using Microsoft.EntityFrameworkCore.Migrations;

namespace MyULibrary.Data.Migrations
{
    public partial class SeedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO dbo.Books(Title, Genre, Author, Stock, PublishDate) VALUES('Cuentos de Barro', 'Fantasia', 'Salarrue', 10, GETDATE())");
            migrationBuilder.Sql("INSERT INTO dbo.Books(Title, Genre, Author, Stock, PublishDate) VALUES('Jicaras Tristes', 'Poetico', 'Alfredo Espino', 5, GETDATE())");
            migrationBuilder.Sql("INSERT INTO dbo.Books(Title, Genre, Author, Stock, PublishDate) VALUES('Tierra de Infancia', 'Poetico', 'Claudia Lars', 6, GETDATE())");
            migrationBuilder.Sql("INSERT INTO dbo.Books(Title, Genre, Author, Stock, PublishDate) VALUES('Andanza y Malandanza', 'Novela', 'Alberto Rivas Bonilla', 20, GETDATE())");
            migrationBuilder.Sql("INSERT INTO dbo.Books(Title, Genre, Author, Stock, PublishDate) VALUES('El Dinero Maldito', 'Novela', 'Alberto Masferrer', 0, GETDATE())");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM dbo.BookUsers");
            migrationBuilder.Sql("DELETE FROM dbo.Books");
        }
    }
}
