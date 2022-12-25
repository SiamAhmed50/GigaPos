using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS.DAL.Migrations
{
    public partial class RelatedUnit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Units_RelatedUnitId",
                table: "Units",
                column: "RelatedUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Units_Units_RelatedUnitId",
                table: "Units",
                column: "RelatedUnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction,
                onUpdate:ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Units_Units_RelatedUnitId",
                table: "Units");

            migrationBuilder.DropIndex(
                name: "IX_Units_RelatedUnitId",
                table: "Units");
        }
    }
}
