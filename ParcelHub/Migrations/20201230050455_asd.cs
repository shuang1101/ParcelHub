using Microsoft.EntityFrameworkCore.Migrations;

namespace ParcelHub.Migrations
{
    public partial class asd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChangePasswordUserModel");

            migrationBuilder.DropTable(
                name: "InValidUser");

            migrationBuilder.DropTable(
                name: "LoginUser");

            migrationBuilder.RenameColumn(
                name: "amount",
                table: "Parcel",
                newName: "Amount");

            migrationBuilder.AddColumn<string>(
                name: "PackageLabelBarCode",
                table: "Parcel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SPTackingNumber",
                table: "Parcel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WechatId",
                table: "Consumer",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PackageLabelBarCode",
                table: "Parcel");

            migrationBuilder.DropColumn(
                name: "SPTackingNumber",
                table: "Parcel");

            migrationBuilder.DropColumn(
                name: "WechatId",
                table: "Consumer");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Parcel",
                newName: "amount");

            migrationBuilder.CreateTable(
                name: "ChangePasswordUserModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConfirmNewPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NewPassword = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangePasswordUserModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InValidUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConfirmPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InValidUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoginUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RememberMe = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginUser", x => x.Id);
                });
        }
    }
}
