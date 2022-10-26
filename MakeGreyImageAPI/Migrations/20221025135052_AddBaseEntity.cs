using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
#pragma warning disable CS1591
namespace MakeGreyImageAPI.Migrations
{
    public partial class AddBaseEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "LocalImagesConvertTask",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "LocalImagesConvertTask",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "LocalImagesConvertTask",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "LocalImagesConvertTask",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "LocalImages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "LocalImages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "LocalImages",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "LocalImages",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "LocalImagesConvertTask");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "LocalImagesConvertTask");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "LocalImagesConvertTask");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "LocalImagesConvertTask");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "LocalImages");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "LocalImages");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "LocalImages");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "LocalImages");
        }
    }
}
