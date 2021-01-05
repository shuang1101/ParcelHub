using Microsoft.EntityFrameworkCore.Migrations;

namespace ParcelHub.Migrations
{
    public partial class addcountrys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "SPWarehouseModel");

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "SPWarehouseModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SPWarehouseModel_CountryId",
                table: "SPWarehouseModel",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_SPWarehouseModel_CountryOfWarehouseModel_CountryId",
                table: "SPWarehouseModel",
                column: "CountryId",
                principalTable: "CountryOfWarehouseModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SPWarehouseModel_CountryOfWarehouseModel_CountryId",
                table: "SPWarehouseModel");

            migrationBuilder.DropIndex(
                name: "IX_SPWarehouseModel_CountryId",
                table: "SPWarehouseModel");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "SPWarehouseModel");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "SPWarehouseModel",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
