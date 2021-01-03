using Microsoft.EntityFrameworkCore.Migrations;

namespace ParcelHub.Migrations
{
    public partial class x : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CountryOfOrigin",
                table: "Parcel",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountryOfOrigin",
                table: "Parcel");
        }
    }
}
