using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace First_Project.Migrations
{
    /// <inheritdoc />
    public partial class newaddcart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "prix",
                table: "CommandeProduits",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "prix",
                table: "CommandeProduits");
        }
    }
}
