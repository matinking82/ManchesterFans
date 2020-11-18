using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ManchesterFans.Persistance.Migrations
{
    public partial class addrelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "LoginId",
                keyValue: 1,
                column: "InsertTime",
                value: new DateTime(2020, 9, 28, 11, 32, 23, 19, DateTimeKind.Local).AddTicks(8216));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "LoginId",
                keyValue: 1,
                column: "InsertTime",
                value: new DateTime(2020, 9, 28, 11, 31, 0, 981, DateTimeKind.Local).AddTicks(1133));
        }
    }
}
