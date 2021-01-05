using Microsoft.EntityFrameworkCore.Migrations;

namespace ParcelHub.Migrations
{
    public partial class browwwss22kkj : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shippment_ServiceProviderUser_SPWarehouseModelId",
                table: "Shippment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceProviderUser",
                table: "ServiceProviderUser");

            migrationBuilder.RenameTable(
                name: "ServiceProviderUser",
                newName: "SPWarehouseModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SPWarehouseModel",
                table: "SPWarehouseModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Shippment_SPWarehouseModel_SPWarehouseModelId",
                table: "Shippment",
                column: "SPWarehouseModelId",
                principalTable: "SPWarehouseModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shippment_SPWarehouseModel_SPWarehouseModelId",
                table: "Shippment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SPWarehouseModel",
                table: "SPWarehouseModel");

            migrationBuilder.RenameTable(
                name: "SPWarehouseModel",
                newName: "ServiceProviderUser");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceProviderUser",
                table: "ServiceProviderUser",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Shippment_ServiceProviderUser_SPWarehouseModelId",
                table: "Shippment",
                column: "SPWarehouseModelId",
                principalTable: "ServiceProviderUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
