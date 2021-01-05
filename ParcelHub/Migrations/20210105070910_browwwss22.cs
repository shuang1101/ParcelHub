using Microsoft.EntityFrameworkCore.Migrations;

namespace ParcelHub.Migrations
{
    public partial class browwwss22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SPWarehouseModelId",
                table: "Parcel");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SPWarehouseModelId",
                table: "Parcel",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
