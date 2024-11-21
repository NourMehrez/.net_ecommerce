using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace First_Project.Migrations
{
    /// <inheritdoc />
    public partial class Cartnew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItems_Products_ProduitIdProduit",
                table: "ShoppingCartItems");

            migrationBuilder.RenameColumn(
                name: "ProduitIdProduit",
                table: "ShoppingCartItems",
                newName: "ProduitId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCartItems_ProduitIdProduit",
                table: "ShoppingCartItems",
                newName: "IX_ShoppingCartItems_ProduitId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItems_Products_ProduitId",
                table: "ShoppingCartItems",
                column: "ProduitId",
                principalTable: "Products",
                principalColumn: "IdProduit",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItems_Products_ProduitId",
                table: "ShoppingCartItems");

            migrationBuilder.RenameColumn(
                name: "ProduitId",
                table: "ShoppingCartItems",
                newName: "ProduitIdProduit");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCartItems_ProduitId",
                table: "ShoppingCartItems",
                newName: "IX_ShoppingCartItems_ProduitIdProduit");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItems_Products_ProduitIdProduit",
                table: "ShoppingCartItems",
                column: "ProduitIdProduit",
                principalTable: "Products",
                principalColumn: "IdProduit",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
