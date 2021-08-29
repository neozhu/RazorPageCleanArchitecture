using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CleanArchitecture.Razor.Infrastructure.Persistence.Migrations
{
    public partial class modify_Approvaldata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "ApprovalDatas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ApprovalDatas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "ApprovalDatas",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "ApprovalDatas",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "ApprovalDatas");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ApprovalDatas");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "ApprovalDatas");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "ApprovalDatas");
        }
    }
}
