using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ManchesterFans.Persistance.Migrations
{
    public partial class editheaderlinks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayTest",
                table: "HeaderLinks");

            migrationBuilder.AddColumn<string>(
                name: "DisplayText",
                table: "HeaderLinks",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "LoginId",
                keyValue: 1,
                column: "InsertTime",
                value: new DateTime(2020, 9, 27, 21, 7, 2, 74, DateTimeKind.Local).AddTicks(5338));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayText",
                table: "HeaderLinks");

            migrationBuilder.AddColumn<string>(
                name: "DisplayTest",
                table: "HeaderLinks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "LoginId",
                keyValue: 1,
                column: "InsertTime",
                value: new DateTime(2020, 9, 26, 18, 9, 14, 544, DateTimeKind.Local).AddTicks(7952));
        }
    }
}
