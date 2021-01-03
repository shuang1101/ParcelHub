using Microsoft.EntityFrameworkCore.Migrations;

namespace ParcelHub.Migrations
{
    public partial class ak : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ModelIsvalid",
                table: "Shippment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ModelIsvalid",
                table: "ServiceProviderUser",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ModelIsvalid",
                table: "Parcel",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ModelIsvalid",
                table: "Invoice",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ModelIsvalid",
                table: "ConsumerAddress",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ModelIsvalid",
                table: "Consumer",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModelIsvalid",
                table: "Shippment");

            migrationBuilder.DropColumn(
                name: "ModelIsvalid",
                table: "ServiceProviderUser");

            migrationBuilder.DropColumn(
                name: "ModelIsvalid",
                table: "Parcel");

            migrationBuilder.DropColumn(
                name: "ModelIsvalid",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "ModelIsvalid",
                table: "ConsumerAddress");

            migrationBuilder.DropColumn(
                name: "ModelIsvalid",
                table: "Consumer");
        }
    }
}
