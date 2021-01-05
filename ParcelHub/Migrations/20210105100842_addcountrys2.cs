using Microsoft.EntityFrameworkCore.Migrations;

namespace ParcelHub.Migrations
{
    public partial class addcountrys2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SPWarehouseModel_CountryOfWarehouseModel_CountryId",
                table: "SPWarehouseModel");

            migrationBuilder.RenameColumn(
                name: "CountryId",
                table: "SPWarehouseModel",
                newName: "CountryName");

            migrationBuilder.RenameIndex(
                name: "IX_SPWarehouseModel_CountryId",
                table: "SPWarehouseModel",
                newName: "IX_SPWarehouseModel_CountryName");

            migrationBuilder.AddForeignKey(
                name: "FK_SPWarehouseModel_CountryOfWarehouseModel_CountryName",
                table: "SPWarehouseModel",
                column: "CountryName",
                principalTable: "CountryOfWarehouseModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SPWarehouseModel_CountryOfWarehouseModel_CountryName",
                table: "SPWarehouseModel");

            migrationBuilder.RenameColumn(
                name: "CountryName",
                table: "SPWarehouseModel",
                newName: "CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_SPWarehouseModel_CountryName",
                table: "SPWarehouseModel",
                newName: "IX_SPWarehouseModel_CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_SPWarehouseModel_CountryOfWarehouseModel_CountryId",
                table: "SPWarehouseModel",
                column: "CountryId",
                principalTable: "CountryOfWarehouseModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
