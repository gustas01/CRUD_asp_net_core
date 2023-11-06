using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatalogAPI.Migrations {
  /// <inheritdoc />
  public partial class populateTableProduct : Migration {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder mb) {
      mb.Sql("Insert into \"Products\"(\"Name\", \"Description\", \"Price\", \"ImageURL\", \"storage\", \"RegisterDate\", \"CategoryId\") Values('Coca-Cola Diet', 'Refrigerante de cola 350 ml', 5.45, 'cocacola.jpg', 50, now(), 1)");

      mb.Sql("Insert into \"Products\"(\"Name\", \"Description\", \"Price\", \"ImageURL\", \"storage\", \"RegisterDate\", \"CategoryId\") Values('x-tudão', 'lanche grande', 10.00, 'xtudao.jpg', 50, now(), 2)");

      mb.Sql("Insert into \"Products\"(\"Name\", \"Description\", \"Price\", \"ImageURL\", \"storage\", \"RegisterDate\", \"CategoryId\") Values('Pudim', 'Pudim de chocolate 150 ml', 6.69, 'pudim.jpg', 50, now(), 3)");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder mb) {
      mb.Sql("delete from \"Products\"");
    }
  }
}
