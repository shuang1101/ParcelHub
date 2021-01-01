using Microsoft.EntityFrameworkCore.Migrations;

namespace ParcelHub.Migrations
{
    public partial class addxxxsskh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ItemValue",
                table: "Parcel",
                newName: "TotalValue");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Parcel",
                newName: "NumberOfUnits");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalValue",
                table: "Parcel",
                newName: "ItemValue");

            migrationBuilder.RenameColumn(
                name: "NumberOfUnits",
                table: "Parcel",
                newName: "Amount");
        }
    }
}
