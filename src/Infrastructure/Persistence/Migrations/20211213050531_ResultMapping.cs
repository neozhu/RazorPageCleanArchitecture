using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Razor.Infrastructure.Persistence.Migrations
{
    public partial class ResultMapping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ResultMappings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MigrationProjectId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RelevantMock = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LegacySystem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RelevantObjects = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Team = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MigrationApproach = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TemplateFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TemplateDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FieldParameters = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultMappings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResultMappings_MigrationProjects_MigrationProjectId",
                        column: x => x.MigrationProjectId,
                        principalTable: "MigrationProjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResultMappingDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MigrationProjectId = table.Column<int>(type: "int", nullable: false),
                    ResultMappingId = table.Column<int>(type: "int", nullable: false),
                    FieldData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Verify = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VerifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultMappingDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResultMappingDatas_ResultMappings_ResultMappingId",
                        column: x => x.ResultMappingId,
                        principalTable: "ResultMappings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResultMappingDatas_ResultMappingId",
                table: "ResultMappingDatas",
                column: "ResultMappingId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultMappings_MigrationProjectId",
                table: "ResultMappings",
                column: "MigrationProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResultMappingDatas");

            migrationBuilder.DropTable(
                name: "ResultMappings");
        }
    }
}
