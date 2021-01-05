using Microsoft.EntityFrameworkCore.Migrations;

namespace ParcelHub.Migrations
{
    public partial class addcountry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "ConsumerAddress");

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "ConsumerAddress",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CountryOfWarehouseModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryOfWarehouseModel", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConsumerAddress_CountryId",
                table: "ConsumerAddress",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConsumerAddress_CountryOfWarehouseModel_CountryId",
                table: "ConsumerAddress",
                column: "CountryId",
                principalTable: "CountryOfWarehouseModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsumerAddress_CountryOfWarehouseModel_CountryId",
                table: "ConsumerAddress");

            migrationBuilder.DropTable(
                name: "CountryOfWarehouseModel");

            migrationBuilder.DropIndex(
                name: "IX_ConsumerAddress_CountryId",
                table: "ConsumerAddress");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "ConsumerAddress");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "ConsumerAddress",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
