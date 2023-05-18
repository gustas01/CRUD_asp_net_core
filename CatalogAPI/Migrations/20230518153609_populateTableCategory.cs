using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatalogAPI.Migrations
{
    /// <inheritdoc />
    public partial class populateTableCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into \"Categories\"(\"Name\", \"ImageURL\") Values('Bebidas', 'bebidas.jpg')");
            mb.Sql("Insert into \"Categories\"(\"Name\", \"ImageURL\") Values('Lanches', 'lanches.jpg')");
            mb.Sql("Insert into \"Categories\"(\"Name\", \"ImageURL\") Values('Sobremesa', 'sobremesa.jpg')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from \"Categories\"");
        }
    }
}
