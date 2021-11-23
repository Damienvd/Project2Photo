using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CMPG323.P2.Services.DataAccess.Migrations
{
    public partial class V2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "FileItems",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCaptured",
                table: "FileItems",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "FileItems",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Extension",
                table: "FileItems",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Geolocation",
                table: "FileItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Guid",
                table: "FileItems",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "Size",
                table: "FileItems",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Tags",
                table: "FileItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "FileItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FileItems_Guid",
                table: "FileItems",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FileItems_UserId",
                table: "FileItems",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FileItems_Users_UserId",
                table: "FileItems",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileItems_Users_UserId",
                table: "FileItems");

            migrationBuilder.DropIndex(
                name: "IX_FileItems_Guid",
                table: "FileItems");

            migrationBuilder.DropIndex(
                name: "IX_FileItems_UserId",
                table: "FileItems");

            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "FileItems");

            migrationBuilder.DropColumn(
                name: "DateCaptured",
                table: "FileItems");

            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "FileItems");

            migrationBuilder.DropColumn(
                name: "Extension",
                table: "FileItems");

            migrationBuilder.DropColumn(
                name: "Geolocation",
                table: "FileItems");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "FileItems");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "FileItems");

            migrationBuilder.DropColumn(
                name: "Tags",
                table: "FileItems");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "FileItems");
        }
    }
}
