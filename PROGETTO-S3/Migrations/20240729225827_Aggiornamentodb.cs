using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PROGETTO_S3.Migrations
{
    /// <inheritdoc />
    public partial class Aggiornamentodb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingridients_Products_ProductId",
                table: "Ingridients");

            migrationBuilder.DropIndex(
                name: "IX_Ingridients_ProductId",
                table: "Ingridients");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Ingridients");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Products",
                newName: "IdProduct");

            migrationBuilder.CreateTable(
                name: "IngridientProduct",
                columns: table => new
                {
                    IngridientsId = table.Column<int>(type: "int", nullable: false),
                    ProductsIdProduct = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngridientProduct", x => new { x.IngridientsId, x.ProductsIdProduct });
                    table.ForeignKey(
                        name: "FK_IngridientProduct_Ingridients_IngridientsId",
                        column: x => x.IngridientsId,
                        principalTable: "Ingridients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngridientProduct_Products_ProductsIdProduct",
                        column: x => x.ProductsIdProduct,
                        principalTable: "Products",
                        principalColumn: "IdProduct",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngridientProduct_ProductsIdProduct",
                table: "IngridientProduct",
                column: "ProductsIdProduct");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngridientProduct");

            migrationBuilder.RenameColumn(
                name: "IdProduct",
                table: "Products",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Ingridients",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ingridients_ProductId",
                table: "Ingridients",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingridients_Products_ProductId",
                table: "Ingridients",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
