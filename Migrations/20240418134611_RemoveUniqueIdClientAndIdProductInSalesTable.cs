using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CamposDealerCrud.Migrations
{
    public partial class RemoveUniqueIdClientAndIdProductInSalesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Sales_ClientId_ProductId",
                table: "Sales");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Sales_ClientId_ProductId",
                table: "Sales",
                columns: new[] { "ClientId", "ProductId" },
                unique: true);
        }
    }
}
