using Microsoft.EntityFrameworkCore.Migrations;

namespace ParcelHub.Migrations
{
    public partial class browww : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsumerAddress_AspNetUsers_IdentityUserId",
                table: "ConsumerAddress");

            migrationBuilder.DropForeignKey(
                name: "FK_Parcel_AspNetUsers_IdentityUserId",
                table: "Parcel");

            migrationBuilder.DropForeignKey(
                name: "FK_Shippment_AspNetUsers_IdentityUserId",
                table: "Shippment");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Consumer");

            migrationBuilder.RenameColumn(
                name: "IdentityUserId",
                table: "Shippment",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Shippment_IdentityUserId",
                table: "Shippment",
                newName: "IX_Shippment_ApplicationUserId");

            migrationBuilder.RenameColumn(
                name: "Suburb",
                table: "ServiceProviderUser",
                newName: "ReceiverName");

            migrationBuilder.RenameColumn(
                name: "StreeAddress",
                table: "ServiceProviderUser",
                newName: "Mobile");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "ServiceProviderUser",
                newName: "AddressLine3");

            migrationBuilder.RenameColumn(
                name: "IdentityUserId",
                table: "Parcel",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Parcel_IdentityUserId",
                table: "Parcel",
                newName: "IX_Parcel_ApplicationUserId");

            migrationBuilder.RenameColumn(
                name: "IdentityUserId",
                table: "ConsumerAddress",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ConsumerAddress_IdentityUserId",
                table: "ConsumerAddress",
                newName: "IX_ConsumerAddress_ApplicationUserId");

            migrationBuilder.RenameColumn(
                name: "IdentityUserId",
                table: "Consumer",
                newName: "ApplicationUserId");

            migrationBuilder.AddColumn<int>(
                name: "SPWarehouseModelId",
                table: "Shippment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AddressLine1",
                table: "ServiceProviderUser",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AddressLine2",
                table: "ServiceProviderUser",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AirService",
                table: "ServiceProviderUser",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsWarehouseAtOrigin",
                table: "ServiceProviderUser",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LandService",
                table: "ServiceProviderUser",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "OcreanFreightService",
                table: "ServiceProviderUser",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SPWarehouseModelId",
                table: "Parcel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AddColumn<int>(
                name: "AgentCodeId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SPWarehouseModelIdIfUserIsAdmin",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.CreateIndex(
                name: "IX_Shippment_SPWarehouseModelId",
                table: "Shippment",
                column: "SPWarehouseModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Parcel_SPWarehouseModelId",
                table: "Parcel",
                column: "SPWarehouseModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConsumerAddress_AspNetUsers_ApplicationUserId",
                table: "ConsumerAddress",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Parcel_AspNetUsers_ApplicationUserId",
                table: "Parcel",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Parcel_ServiceProviderUser_SPWarehouseModelId",
                table: "Parcel",
                column: "SPWarehouseModelId",
                principalTable: "ServiceProviderUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shippment_AspNetUsers_ApplicationUserId",
                table: "Shippment",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Shippment_ServiceProviderUser_SPWarehouseModelId",
                table: "Shippment",
                column: "SPWarehouseModelId",
                principalTable: "ServiceProviderUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsumerAddress_AspNetUsers_ApplicationUserId",
                table: "ConsumerAddress");

            migrationBuilder.DropForeignKey(
                name: "FK_Parcel_AspNetUsers_ApplicationUserId",
                table: "Parcel");

            migrationBuilder.DropForeignKey(
                name: "FK_Parcel_ServiceProviderUser_SPWarehouseModelId",
                table: "Parcel");

            migrationBuilder.DropForeignKey(
                name: "FK_Shippment_AspNetUsers_ApplicationUserId",
                table: "Shippment");

            migrationBuilder.DropForeignKey(
                name: "FK_Shippment_ServiceProviderUser_SPWarehouseModelId",
                table: "Shippment");

            migrationBuilder.DropIndex(
                name: "IX_Shippment_SPWarehouseModelId",
                table: "Shippment");

            migrationBuilder.DropIndex(
                name: "IX_Parcel_SPWarehouseModelId",
                table: "Parcel");

            migrationBuilder.DropColumn(
                name: "SPWarehouseModelId",
                table: "Shippment");

            migrationBuilder.DropColumn(
                name: "AddressLine1",
                table: "ServiceProviderUser");

            migrationBuilder.DropColumn(
                name: "AddressLine2",
                table: "ServiceProviderUser");

            migrationBuilder.DropColumn(
                name: "AirService",
                table: "ServiceProviderUser");

            migrationBuilder.DropColumn(
                name: "IsWarehouseAtOrigin",
                table: "ServiceProviderUser");

            migrationBuilder.DropColumn(
                name: "LandService",
                table: "ServiceProviderUser");

            migrationBuilder.DropColumn(
                name: "OcreanFreightService",
                table: "ServiceProviderUser");

            migrationBuilder.DropColumn(
                name: "SPWarehouseModelId",
                table: "Parcel");

            migrationBuilder.DropColumn(
                name: "AgentCodeId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SPWarehouseModelIdIfUserIsAdmin",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Shippment",
                newName: "IdentityUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Shippment_ApplicationUserId",
                table: "Shippment",
                newName: "IX_Shippment_IdentityUserId");

            migrationBuilder.RenameColumn(
                name: "ReceiverName",
                table: "ServiceProviderUser",
                newName: "Suburb");

            migrationBuilder.RenameColumn(
                name: "Mobile",
                table: "ServiceProviderUser",
                newName: "StreeAddress");

            migrationBuilder.RenameColumn(
                name: "AddressLine3",
                table: "ServiceProviderUser",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Parcel",
                newName: "IdentityUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Parcel_ApplicationUserId",
                table: "Parcel",
                newName: "IX_Parcel_IdentityUserId");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "ConsumerAddress",
                newName: "IdentityUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ConsumerAddress_ApplicationUserId",
                table: "ConsumerAddress",
                newName: "IX_ConsumerAddress_IdentityUserId");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Consumer",
                newName: "IdentityUserId");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Consumer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_ConsumerAddress_AspNetUsers_IdentityUserId",
                table: "ConsumerAddress",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Parcel_AspNetUsers_IdentityUserId",
                table: "Parcel",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Shippment_AspNetUsers_IdentityUserId",
                table: "Shippment",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
