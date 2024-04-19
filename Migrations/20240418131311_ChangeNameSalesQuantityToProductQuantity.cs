using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CamposDealerCrud.Migrations
{
    public partial class ChangeNameSalesQuantityToProductQuantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SalesQuantity",
                table: "Sales",
                newName: "ProductQuantity");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductQuantity",
                table: "Sales",
                newName: "SalesQuantity");
        }
    }
}
