using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS.DAL.Migrations
{
    public partial class Changes02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Products_ProductId",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_ProductId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Purchases");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseItems_ProductId",
                table: "PurchaseItems",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseItems_Products_ProductId",
                table: "PurchaseItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseItems_Products_ProductId",
                table: "PurchaseItems");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseItems_ProductId",
                table: "PurchaseItems");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Purchases",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_ProductId",
                table: "Purchases",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Products_ProductId",
                table: "Purchases",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
