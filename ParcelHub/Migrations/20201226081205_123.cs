using Microsoft.EntityFrameworkCore.Migrations;

namespace ParcelHub.Migrations
{
    public partial class _123 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsumerAddress_Consumer_Consumeremail",
                table: "ConsumerAddress");

            migrationBuilder.DropForeignKey(
                name: "FK_parcel_Shippment_ShippmentId",
                table: "parcel");

            migrationBuilder.DropForeignKey(
                name: "FK_Shippment_serviceProviderUser_Id",
                table: "Shippment");

            migrationBuilder.DropForeignKey(
                name: "FK_Shippment_shippingContainer_Id",
                table: "Shippment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_shippingContainer",
                table: "shippingContainer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_serviceProviderUser",
                table: "serviceProviderUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_parcel",
                table: "parcel");

            migrationBuilder.RenameTable(
                name: "shippingContainer",
                newName: "ShippingContainer");

            migrationBuilder.RenameTable(
                name: "serviceProviderUser",
                newName: "ServiceProviderUser");

            migrationBuilder.RenameTable(
                name: "parcel",
                newName: "Parcel");

            migrationBuilder.RenameColumn(
                name: "originTrackingNumber",
                table: "Parcel",
                newName: "OriginTrackingNumber");

            migrationBuilder.RenameIndex(
                name: "IX_parcel_ShippmentId",
                table: "Parcel",
                newName: "IX_Parcel_ShippmentId");

            migrationBuilder.RenameColumn(
                name: "streetAddress",
                table: "ConsumerAddress",
                newName: "StreetAddress");

            migrationBuilder.RenameColumn(
                name: "state",
                table: "ConsumerAddress",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "postCode",
                table: "ConsumerAddress",
                newName: "PostCode");

            migrationBuilder.RenameColumn(
                name: "country",
                table: "ConsumerAddress",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "city",
                table: "ConsumerAddress",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "Consumeremail",
                table: "ConsumerAddress",
                newName: "ConsumerEmail");

            migrationBuilder.RenameIndex(
                name: "IX_ConsumerAddress_Consumeremail",
                table: "ConsumerAddress",
                newName: "IX_ConsumerAddress_ConsumerEmail");

            migrationBuilder.RenameColumn(
                name: "mobileNumber",
                table: "Consumer",
                newName: "MobileNumber");

            migrationBuilder.RenameColumn(
                name: "lastName",
                table: "Consumer",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "firstName",
                table: "Consumer",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Consumer",
                newName: "Email");

            migrationBuilder.AddColumn<bool>(
                name: "IsInvoicePaid",
                table: "Invoice",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShippingContainer",
                table: "ShippingContainer",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceProviderUser",
                table: "ServiceProviderUser",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Parcel",
                table: "Parcel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ConsumerAddress_Consumer_ConsumerEmail",
                table: "ConsumerAddress",
                column: "ConsumerEmail",
                principalTable: "Consumer",
                principalColumn: "Email",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Parcel_Shippment_ShippmentId",
                table: "Parcel",
                column: "ShippmentId",
                principalTable: "Shippment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shippment_ServiceProviderUser_Id",
                table: "Shippment",
                column: "Id",
                principalTable: "ServiceProviderUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shippment_ShippingContainer_Id",
                table: "Shippment",
                column: "Id",
                principalTable: "ShippingContainer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsumerAddress_Consumer_ConsumerEmail",
                table: "ConsumerAddress");

            migrationBuilder.DropForeignKey(
                name: "FK_Parcel_Shippment_ShippmentId",
                table: "Parcel");

            migrationBuilder.DropForeignKey(
                name: "FK_Shippment_ServiceProviderUser_Id",
                table: "Shippment");

            migrationBuilder.DropForeignKey(
                name: "FK_Shippment_ShippingContainer_Id",
                table: "Shippment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShippingContainer",
                table: "ShippingContainer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceProviderUser",
                table: "ServiceProviderUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Parcel",
                table: "Parcel");

            migrationBuilder.DropColumn(
                name: "IsInvoicePaid",
                table: "Invoice");

            migrationBuilder.RenameTable(
                name: "ShippingContainer",
                newName: "shippingContainer");

            migrationBuilder.RenameTable(
                name: "ServiceProviderUser",
                newName: "serviceProviderUser");

            migrationBuilder.RenameTable(
                name: "Parcel",
                newName: "parcel");

            migrationBuilder.RenameColumn(
                name: "OriginTrackingNumber",
                table: "parcel",
                newName: "originTrackingNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Parcel_ShippmentId",
                table: "parcel",
                newName: "IX_parcel_ShippmentId");

            migrationBuilder.RenameColumn(
                name: "StreetAddress",
                table: "ConsumerAddress",
                newName: "streetAddress");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "ConsumerAddress",
                newName: "state");

            migrationBuilder.RenameColumn(
                name: "PostCode",
                table: "ConsumerAddress",
                newName: "postCode");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "ConsumerAddress",
                newName: "country");

            migrationBuilder.RenameColumn(
                name: "ConsumerEmail",
                table: "ConsumerAddress",
                newName: "Consumeremail");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "ConsumerAddress",
                newName: "city");

            migrationBuilder.RenameIndex(
                name: "IX_ConsumerAddress_ConsumerEmail",
                table: "ConsumerAddress",
                newName: "IX_ConsumerAddress_Consumeremail");

            migrationBuilder.RenameColumn(
                name: "MobileNumber",
                table: "Consumer",
                newName: "mobileNumber");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Consumer",
                newName: "lastName");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Consumer",
                newName: "firstName");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Consumer",
                newName: "email");

            migrationBuilder.AddPrimaryKey(
                name: "PK_shippingContainer",
                table: "shippingContainer",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_serviceProviderUser",
                table: "serviceProviderUser",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_parcel",
                table: "parcel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ConsumerAddress_Consumer_Consumeremail",
                table: "ConsumerAddress",
                column: "Consumeremail",
                principalTable: "Consumer",
                principalColumn: "email",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_parcel_Shippment_ShippmentId",
                table: "parcel",
                column: "ShippmentId",
                principalTable: "Shippment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shippment_serviceProviderUser_Id",
                table: "Shippment",
                column: "Id",
                principalTable: "serviceProviderUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shippment_shippingContainer_Id",
                table: "Shippment",
                column: "Id",
                principalTable: "shippingContainer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
