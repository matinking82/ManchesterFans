using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ManchesterFans.Persistance.Migrations
{
    public partial class AddrelationsPageComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentCommentId",
                table: "PageComments",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "LoginId",
                keyValue: 1,
                column: "InsertTime",
                value: new DateTime(2020, 11, 5, 20, 35, 44, 40, DateTimeKind.Local).AddTicks(4397));

            migrationBuilder.CreateIndex(
                name: "IX_PageComments_ParentCommentId",
                table: "PageComments",
                column: "ParentCommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_PageComments_PageComments_ParentCommentId",
                table: "PageComments",
                column: "ParentCommentId",
                principalTable: "PageComments",
                principalColumn: "CommentId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageComments_PageComments_ParentCommentId",
                table: "PageComments");

            migrationBuilder.DropIndex(
                name: "IX_PageComments_ParentCommentId",
                table: "PageComments");

            migrationBuilder.DropColumn(
                name: "ParentCommentId",
                table: "PageComments");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "LoginId",
                keyValue: 1,
                column: "InsertTime",
                value: new DateTime(2020, 11, 5, 19, 45, 47, 442, DateTimeKind.Local).AddTicks(4573));
        }
    }
}
