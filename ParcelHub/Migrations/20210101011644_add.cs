using Microsoft.EntityFrameworkCore.Migrations;

namespace ParcelHub.Migrations
{
    public partial class add : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "JobLastEdit",
                table: "Parcel",
                newName: "DateTimeJobLastEdit");

            migrationBuilder.RenameColumn(
                name: "JobCreated",
                table: "Parcel",
                newName: "DateTimeJobCreated");

            migrationBuilder.RenameColumn(
                name: "Inbound",
                table: "Parcel",
                newName: "DateTimeInboundOrigin");

            migrationBuilder.RenameColumn(
                name: "ArriveInDestination",
                table: "Parcel",
                newName: "DateTimeArriveInDestination");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTimeJobLastEdit",
                table: "Parcel",
                newName: "JobLastEdit");

            migrationBuilder.RenameColumn(
                name: "DateTimeJobCreated",
                table: "Parcel",
                newName: "JobCreated");

            migrationBuilder.RenameColumn(
                name: "DateTimeInboundOrigin",
                table: "Parcel",
                newName: "Inbound");

            migrationBuilder.RenameColumn(
                name: "DateTimeArriveInDestination",
                table: "Parcel",
                newName: "ArriveInDestination");
        }
    }
}
