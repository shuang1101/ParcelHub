using Microsoft.EntityFrameworkCore.Migrations;

namespace ParcelHub.Migrations
{
    public partial class init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consumer_AspNetUsers_IdentityUserId1",
                table: "Consumer");

            migrationBuilder.DropIndex(
                name: "IX_Consumer_IdentityUserId1",
                table: "Consumer");

            migrationBuilder.DropColumn(
                name: "IdentityUserId1",
                table: "Consumer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentityUserId1",
                table: "Consumer",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Consumer_IdentityUserId1",
                table: "Consumer",
                column: "IdentityUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumer_AspNetUsers_IdentityUserId1",
                table: "Consumer",
                column: "IdentityUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
