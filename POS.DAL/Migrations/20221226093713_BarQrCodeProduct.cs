using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS.DAL.Migrations
{
    public partial class BarQrCodeProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BarcodeUrl",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QrCodeUrl",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BarcodeUrl",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "QrCodeUrl",
                table: "Products");
        }
    }
}
