using Microsoft.EntityFrameworkCore.Migrations;

namespace ParcelHub.Migrations
{
    public partial class @try : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentityUserId",
                table: "ConsumerAddress",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConsumerAddress_IdentityUserId",
                table: "ConsumerAddress",
                column: "IdentityUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConsumerAddress_AspNetUsers_IdentityUserId",
                table: "ConsumerAddress",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsumerAddress_AspNetUsers_IdentityUserId",
                table: "ConsumerAddress");

            migrationBuilder.DropIndex(
                name: "IX_ConsumerAddress_IdentityUserId",
                table: "ConsumerAddress");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "ConsumerAddress");
        }
    }
}
