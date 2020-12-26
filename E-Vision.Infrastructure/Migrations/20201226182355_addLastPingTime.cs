using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace E_Vision.Infrastructure.Migrations
{
    public partial class addLastPingTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastPingTime",
                table: "Vehicle",
                type: "datetime2",
                nullable: false,
                defaultValue:  DateTime.UtcNow);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastPingTime",
                table: "Vehicle");
        }
    }
}
