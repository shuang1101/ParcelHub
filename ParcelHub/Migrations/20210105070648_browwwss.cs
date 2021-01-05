using Microsoft.EntityFrameworkCore.Migrations;

namespace ParcelHub.Migrations
{
    public partial class browwwss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parcel_ServiceProviderUser_SPWarehouseModelId",
                table: "Parcel");

            migrationBuilder.DropIndex(
                name: "IX_Parcel_SPWarehouseModelId",
                table: "Parcel");

            migrationBuilder.DropColumn(
                name: "CountryOfOrigin",
                table: "Parcel");

            migrationBuilder.RenameColumn(
                name: "DestinationAddressId",
                table: "Parcel",
                newName: "OriginSPWarehouseModelId");

            migrationBuilder.AddColumn<int>(
                name: "ConsumerAddressId",
                table: "Parcel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DestinatioSPWarehouseModelnId",
                table: "Parcel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Parcel_ConsumerAddressId",
                table: "Parcel",
                column: "ConsumerAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parcel_ConsumerAddress_ConsumerAddressId",
                table: "Parcel",
                column: "ConsumerAddressId",
                principalTable: "ConsumerAddress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parcel_ConsumerAddress_ConsumerAddressId",
                table: "Parcel");

            migrationBuilder.DropIndex(
                name: "IX_Parcel_ConsumerAddressId",
                table: "Parcel");

            migrationBuilder.DropColumn(
                name: "ConsumerAddressId",
                table: "Parcel");

            migrationBuilder.DropColumn(
                name: "DestinatioSPWarehouseModelnId",
                table: "Parcel");

            migrationBuilder.RenameColumn(
                name: "OriginSPWarehouseModelId",
                table: "Parcel",
                newName: "DestinationAddressId");

            migrationBuilder.AddColumn<string>(
                name: "CountryOfOrigin",
                table: "Parcel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parcel_SPWarehouseModelId",
                table: "Parcel",
                column: "SPWarehouseModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parcel_ServiceProviderUser_SPWarehouseModelId",
                table: "Parcel",
                column: "SPWarehouseModelId",
                principalTable: "ServiceProviderUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
