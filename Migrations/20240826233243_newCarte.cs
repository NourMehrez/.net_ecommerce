using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace First_Project.Migrations
{
    /// <inheritdoc />
    public partial class newCarte : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "prix",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "prix",
                table: "Products");
        }
    }
}
