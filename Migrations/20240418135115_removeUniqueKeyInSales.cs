using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalesCrud.Migrations
{
    public partial class removeUniqueKeyInSales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Sales",
                table: "Sales");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sales",
                table: "Sales",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_ClientId",
                table: "Sales",
                column: "ClientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Sales",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_ClientId",
                table: "Sales");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sales",
                table: "Sales",
                columns: new[] { "ClientId", "ProductId" });
        }
    }
}
