using Microsoft.EntityFrameworkCore.Migrations;

namespace ParcelHub.Migrations
{
    public partial class browns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OcreanFreightService",
                table: "SPWarehouseModel",
                newName: "OceanFreightService");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OceanFreightService",
                table: "SPWarehouseModel",
                newName: "OcreanFreightService");
        }
    }
}
