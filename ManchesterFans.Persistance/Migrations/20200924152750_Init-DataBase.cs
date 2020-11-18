using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ManchesterFans.Persistance.Migrations
{
    public partial class InitDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Headers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SiteName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Headers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PageGroups",
                columns: table => new
                {
                    GroupId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageGroups", x => x.GroupId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    LoginId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Level = table.Column<int>(nullable: false),
                    image = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.LoginId);
                });

            migrationBuilder.CreateTable(
                name: "HeaderLinks",
                columns: table => new
                {
                    LinkId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisplayTest = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    HeaderId = table.Column<int>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeaderLinks", x => x.LinkId);
                    table.ForeignKey(
                        name: "FK_HeaderLinks_Headers_HeaderId",
                        column: x => x.HeaderId,
                        principalTable: "Headers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    PageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    ShortDescribtion = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    ImageName = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    Tags = table.Column<string>(nullable: true),
                    Visits = table.Column<int>(nullable: false),
                    Likes = table.Column<int>(nullable: false),
                    PageGroupGroupId = table.Column<int>(nullable: true),
                    GroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.PageId);
                    table.ForeignKey(
                        name: "FK_Pages_PageGroups_PageGroupGroupId",
                        column: x => x.PageGroupGroupId,
                        principalTable: "PageGroups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PageComments",
                columns: table => new
                {
                    CommentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    IsAccepted = table.Column<bool>(nullable: false),
                    Reply = table.Column<int>(nullable: false),
                    Like = table.Column<int>(nullable: false),
                    PageId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageComments", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_PageComments_Pages_PageId",
                        column: x => x.PageId,
                        principalTable: "Pages",
                        principalColumn: "PageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PageComments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "LoginId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "LoginId", "Level", "Password", "Username", "image" },
                values: new object[] { 1, 10, "9215625891", "MatinKing", "Default.png" });

            migrationBuilder.CreateIndex(
                name: "IX_HeaderLinks_HeaderId",
                table: "HeaderLinks",
                column: "HeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_PageComments_PageId",
                table: "PageComments",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_PageComments_UserId",
                table: "PageComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_PageGroupGroupId",
                table: "Pages",
                column: "PageGroupGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HeaderLinks");

            migrationBuilder.DropTable(
                name: "PageComments");

            migrationBuilder.DropTable(
                name: "Headers");

            migrationBuilder.DropTable(
                name: "Pages");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "PageGroups");
        }
    }
}
