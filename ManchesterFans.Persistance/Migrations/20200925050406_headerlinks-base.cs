using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ManchesterFans.Persistance.Migrations
{
    public partial class headerlinksbase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "InsertTime",
                table: "HeaderLinks",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsRemoved",
                table: "HeaderLinks",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "RemoveTime",
                table: "HeaderLinks",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateTime",
                table: "HeaderLinks",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "LoginId",
                keyValue: 1,
                column: "InsertTime",
                value: new DateTime(2020, 9, 25, 8, 34, 6, 8, DateTimeKind.Local).AddTicks(6786));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InsertTime",
                table: "HeaderLinks");

            migrationBuilder.DropColumn(
                name: "IsRemoved",
                table: "HeaderLinks");

            migrationBuilder.DropColumn(
                name: "RemoveTime",
                table: "HeaderLinks");

            migrationBuilder.DropColumn(
                name: "UpdateTime",
                table: "HeaderLinks");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "LoginId",
                keyValue: 1,
                column: "InsertTime",
                value: new DateTime(2020, 9, 25, 8, 27, 39, 878, DateTimeKind.Local).AddTicks(4875));
        }
    }
}
