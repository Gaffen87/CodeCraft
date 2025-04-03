using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeCraftApi.Migrations
{
    /// <inheritdoc />
    public partial class TestContentToJsonb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "content",
                table: "tests",
                type: "jsonb",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "json");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "content",
                table: "tests",
                type: "json",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "jsonb");
        }
    }
}
