using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ParcelHub.Migrations
{
    public partial class akbbasssd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateTimeJobCreated",
                table: "Shippment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTimeJobCreated",
                table: "Shippment");
        }
    }
}
