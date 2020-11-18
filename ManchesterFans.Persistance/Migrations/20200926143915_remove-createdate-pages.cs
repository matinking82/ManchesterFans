using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ManchesterFans.Persistance.Migrations
{
    public partial class removecreatedatepages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Pages");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "LoginId",
                keyValue: 1,
                columns: new[] { "InsertTime", "Password" },
                values: new object[] { new DateTime(2020, 9, 26, 18, 9, 14, 544, DateTimeKind.Local).AddTicks(7952), "7974fbb4617e663ad62f9123bf29cbd325bef0c3a15b8d52a5973e51a47c9a0c" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Pages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "LoginId",
                keyValue: 1,
                columns: new[] { "InsertTime", "Password" },
                values: new object[] { new DateTime(2020, 9, 25, 8, 34, 6, 8, DateTimeKind.Local).AddTicks(6786), "9215625891" });
        }
    }
}
