using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalesCrud.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnStatusInSales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Sales",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Sales");
        }
    }
}
