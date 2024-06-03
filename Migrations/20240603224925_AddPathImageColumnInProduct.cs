using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalesCrud.Migrations
{
    /// <inheritdoc />
    public partial class AddPathImageColumnInProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PathImage",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PathImage",
                table: "Products");
        }
    }
}
