using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Razor.Infrastructure.Persistence.Migrations
{
    public partial class add_field20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Field1",
                table: "ResultMappingDatas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Field10",
                table: "ResultMappingDatas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Field11",
                table: "ResultMappingDatas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Field12",
                table: "ResultMappingDatas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Field13",
                table: "ResultMappingDatas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Field14",
                table: "ResultMappingDatas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Field15",
                table: "ResultMappingDatas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Field16",
                table: "ResultMappingDatas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Field17",
                table: "ResultMappingDatas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Field18",
                table: "ResultMappingDatas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Field19",
                table: "ResultMappingDatas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Field2",
                table: "ResultMappingDatas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Field20",
                table: "ResultMappingDatas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Field3",
                table: "ResultMappingDatas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Field4",
                table: "ResultMappingDatas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Field5",
                table: "ResultMappingDatas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Field6",
                table: "ResultMappingDatas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Field7",
                table: "ResultMappingDatas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Field8",
                table: "ResultMappingDatas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Field9",
                table: "ResultMappingDatas",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Field1",
                table: "ResultMappingDatas");

            migrationBuilder.DropColumn(
                name: "Field10",
                table: "ResultMappingDatas");

            migrationBuilder.DropColumn(
                name: "Field11",
                table: "ResultMappingDatas");

            migrationBuilder.DropColumn(
                name: "Field12",
                table: "ResultMappingDatas");

            migrationBuilder.DropColumn(
                name: "Field13",
                table: "ResultMappingDatas");

            migrationBuilder.DropColumn(
                name: "Field14",
                table: "ResultMappingDatas");

            migrationBuilder.DropColumn(
                name: "Field15",
                table: "ResultMappingDatas");

            migrationBuilder.DropColumn(
                name: "Field16",
                table: "ResultMappingDatas");

            migrationBuilder.DropColumn(
                name: "Field17",
                table: "ResultMappingDatas");

            migrationBuilder.DropColumn(
                name: "Field18",
                table: "ResultMappingDatas");

            migrationBuilder.DropColumn(
                name: "Field19",
                table: "ResultMappingDatas");

            migrationBuilder.DropColumn(
                name: "Field2",
                table: "ResultMappingDatas");

            migrationBuilder.DropColumn(
                name: "Field20",
                table: "ResultMappingDatas");

            migrationBuilder.DropColumn(
                name: "Field3",
                table: "ResultMappingDatas");

            migrationBuilder.DropColumn(
                name: "Field4",
                table: "ResultMappingDatas");

            migrationBuilder.DropColumn(
                name: "Field5",
                table: "ResultMappingDatas");

            migrationBuilder.DropColumn(
                name: "Field6",
                table: "ResultMappingDatas");

            migrationBuilder.DropColumn(
                name: "Field7",
                table: "ResultMappingDatas");

            migrationBuilder.DropColumn(
                name: "Field8",
                table: "ResultMappingDatas");

            migrationBuilder.DropColumn(
                name: "Field9",
                table: "ResultMappingDatas");
        }
    }
}
