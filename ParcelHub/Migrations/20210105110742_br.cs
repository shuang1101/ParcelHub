using Microsoft.EntityFrameworkCore.Migrations;

namespace ParcelHub.Migrations
{
    public partial class br : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SPUserModel",
                columns: table => new
                {
                    ApplicationUserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<int>(type: "int", nullable: false),
                    SPWarehouseModelId = table.Column<int>(type: "int", nullable: false),
                    IsValid = table.Column<bool>(type: "bit", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SPUserModel", x => x.ApplicationUserId);
                    table.ForeignKey(
                        name: "FK_SPUserModel_SPWarehouseModel_SPWarehouseModelId",
                        column: x => x.SPWarehouseModelId,
                        principalTable: "SPWarehouseModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SPUserModel_SPWarehouseModelId",
                table: "SPUserModel",
                column: "SPWarehouseModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SPUserModel");
        }
    }
}
