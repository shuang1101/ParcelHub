using Microsoft.EntityFrameworkCore.Migrations;

namespace ParcelHub.Migrations
{
    public partial class addcountrys22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SPWarehouseModel_CountryOfWarehouseModel_CountryName",
                table: "SPWarehouseModel");

            migrationBuilder.DropIndex(
                name: "IX_SPWarehouseModel_CountryName",
                table: "SPWarehouseModel");

            migrationBuilder.DropColumn(
                name: "IsWarehouseAtOrigin",
                table: "SPWarehouseModel");

            migrationBuilder.RenameColumn(
                name: "CountryName",
                table: "SPWarehouseModel",
                newName: "CountryId");

            migrationBuilder.AddColumn<int>(
                name: "CountryOfWarehouseModelId",
                table: "SPWarehouseModel",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SPWarehouseModel_CountryOfWarehouseModelId",
                table: "SPWarehouseModel",
                column: "CountryOfWarehouseModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_SPWarehouseModel_CountryOfWarehouseModel_CountryOfWarehouseModelId",
                table: "SPWarehouseModel",
                column: "CountryOfWarehouseModelId",
                principalTable: "CountryOfWarehouseModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SPWarehouseModel_CountryOfWarehouseModel_CountryOfWarehouseModelId",
                table: "SPWarehouseModel");

            migrationBuilder.DropIndex(
                name: "IX_SPWarehouseModel_CountryOfWarehouseModelId",
                table: "SPWarehouseModel");

            migrationBuilder.DropColumn(
                name: "CountryOfWarehouseModelId",
                table: "SPWarehouseModel");

            migrationBuilder.RenameColumn(
                name: "CountryId",
                table: "SPWarehouseModel",
                newName: "CountryName");

            migrationBuilder.AddColumn<bool>(
                name: "IsWarehouseAtOrigin",
                table: "SPWarehouseModel",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_SPWarehouseModel_CountryName",
                table: "SPWarehouseModel",
                column: "CountryName");

            migrationBuilder.AddForeignKey(
                name: "FK_SPWarehouseModel_CountryOfWarehouseModel_CountryName",
                table: "SPWarehouseModel",
                column: "CountryName",
                principalTable: "CountryOfWarehouseModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
