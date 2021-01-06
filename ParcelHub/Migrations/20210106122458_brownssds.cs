using Microsoft.EntityFrameworkCore.Migrations;

namespace ParcelHub.Migrations
{
    public partial class brownssds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CountryOfWarehouseModelId",
                table: "ConsumerAddress",
                newName: "CountryOfWarehouseModelIdAtDestination");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CountryOfWarehouseModelIdAtDestination",
                table: "ConsumerAddress",
                newName: "CountryOfWarehouseModelId");
        }
    }
}
