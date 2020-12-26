using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ParcelHub.Migrations
{
    public partial class initallmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Consumeremail",
                table: "ConsumerAddress",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Consumer",
                columns: table => new
                {
                    email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsValid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consumer", x => x.email);
                });

            migrationBuilder.CreateTable(
                name: "serviceProviderUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Suburb = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StreeAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_serviceProviderUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "shippingContainer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContainerNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ETA = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ETD = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Vessel = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shippingContainer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shippment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    SPTackingNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Origin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequiredInsurance = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shippment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shippment_Consumer_Email",
                        column: x => x.Email,
                        principalTable: "Consumer",
                        principalColumn: "email",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Shippment_serviceProviderUser_Id",
                        column: x => x.Id,
                        principalTable: "serviceProviderUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shippment_shippingContainer_Id",
                        column: x => x.Id,
                        principalTable: "shippingContainer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    TotalCharge = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoice_Shippment_Id",
                        column: x => x.Id,
                        principalTable: "Shippment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "parcel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShippmentId = table.Column<int>(type: "int", nullable: false),
                    originTrackingNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstimateWeight = table.Column<float>(type: "real", nullable: false),
                    ActualWeight = table.Column<float>(type: "real", nullable: false),
                    EstimateVolume = table.Column<float>(type: "real", nullable: false),
                    ActualVolume = table.Column<float>(type: "real", nullable: false),
                    ItemValue = table.Column<float>(type: "real", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Inbound = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_parcel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_parcel_Shippment_ShippmentId",
                        column: x => x.ShippmentId,
                        principalTable: "Shippment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConsumerAddress_Consumeremail",
                table: "ConsumerAddress",
                column: "Consumeremail");

            migrationBuilder.CreateIndex(
                name: "IX_parcel_ShippmentId",
                table: "parcel",
                column: "ShippmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Shippment_Email",
                table: "Shippment",
                column: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_ConsumerAddress_Consumer_Consumeremail",
                table: "ConsumerAddress",
                column: "Consumeremail",
                principalTable: "Consumer",
                principalColumn: "email",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsumerAddress_Consumer_Consumeremail",
                table: "ConsumerAddress");

            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropTable(
                name: "parcel");

            migrationBuilder.DropTable(
                name: "Shippment");

            migrationBuilder.DropTable(
                name: "Consumer");

            migrationBuilder.DropTable(
                name: "serviceProviderUser");

            migrationBuilder.DropTable(
                name: "shippingContainer");

            migrationBuilder.DropIndex(
                name: "IX_ConsumerAddress_Consumeremail",
                table: "ConsumerAddress");

            migrationBuilder.DropColumn(
                name: "Consumeremail",
                table: "ConsumerAddress");
        }
    }
}
