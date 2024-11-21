using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace First_Project.Migrations
{
    /// <inheritdoc />
    public partial class addPrix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "prix",
                table: "Commandes",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "prix",
                table: "Commandes");
        }
    }
}
