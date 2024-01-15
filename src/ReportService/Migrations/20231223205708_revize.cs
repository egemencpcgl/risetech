using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReportManagementService.Migrations
{
    /// <inheritdoc />
    public partial class revize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Parameters",
                table: "Reports");

            migrationBuilder.AddColumn<string>(
                name: "ReportStatus",
                table: "Reports",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReportStatus",
                table: "Reports");

            migrationBuilder.AddColumn<string[]>(
                name: "Parameters",
                table: "Reports",
                type: "text[]",
                nullable: false,
                defaultValue: new string[0]);
        }
    }
}
