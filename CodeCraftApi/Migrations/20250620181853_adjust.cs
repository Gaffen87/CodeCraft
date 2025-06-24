using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeCraftApi.Migrations
{
    /// <inheritdoc />
    public partial class adjust : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "progress",
                table: "exercise_progress",
                newName: "completed");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "completed",
                table: "exercise_progress",
                newName: "progress");
        }
    }
}
