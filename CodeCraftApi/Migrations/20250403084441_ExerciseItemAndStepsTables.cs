using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeCraftApi.Migrations
{
    /// <inheritdoc />
    public partial class ExerciseItemAndStepsTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_exercises_exercises_parent_exercise_id",
                table: "exercises");

            migrationBuilder.DropIndex(
                name: "ix_exercises_parent_exercise_id",
                table: "exercises");

            migrationBuilder.DropColumn(
                name: "contraints",
                table: "exercises");

            migrationBuilder.DropColumn(
                name: "description",
                table: "exercises");

            migrationBuilder.DropColumn(
                name: "description_short",
                table: "exercises");

            migrationBuilder.DropColumn(
                name: "hints",
                table: "exercises");

            migrationBuilder.DropColumn(
                name: "parent_exercise_id",
                table: "exercises");

            migrationBuilder.AddColumn<Guid>(
                name: "exercise_step_id",
                table: "tests",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "exercise_item",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    number = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    description_short = table.Column<string>(type: "text", nullable: false),
                    contraints = table.Column<string>(type: "text", nullable: false),
                    hints = table.Column<string>(type: "text", nullable: false),
                    exercise_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_exercise_item", x => x.id);
                    table.ForeignKey(
                        name: "fk_exercise_item_exercises_exercise_id",
                        column: x => x.exercise_id,
                        principalTable: "exercises",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "exercise_step",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    description_short = table.Column<string>(type: "text", nullable: false),
                    contraints = table.Column<string>(type: "text", nullable: false),
                    hints = table.Column<string>(type: "text", nullable: false),
                    exercise_item_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_exercise_step", x => x.id);
                    table.ForeignKey(
                        name: "fk_exercise_step_exercise_item_exercise_item_id",
                        column: x => x.exercise_item_id,
                        principalTable: "exercise_item",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_tests_exercise_step_id",
                table: "tests",
                column: "exercise_step_id");

            migrationBuilder.CreateIndex(
                name: "ix_exercise_item_exercise_id",
                table: "exercise_item",
                column: "exercise_id");

            migrationBuilder.CreateIndex(
                name: "ix_exercise_step_exercise_item_id",
                table: "exercise_step",
                column: "exercise_item_id");

            migrationBuilder.AddForeignKey(
                name: "fk_tests_exercise_step_exercise_step_id",
                table: "tests",
                column: "exercise_step_id",
                principalTable: "exercise_step",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_tests_exercise_step_exercise_step_id",
                table: "tests");

            migrationBuilder.DropTable(
                name: "exercise_step");

            migrationBuilder.DropTable(
                name: "exercise_item");

            migrationBuilder.DropIndex(
                name: "ix_tests_exercise_step_id",
                table: "tests");

            migrationBuilder.DropColumn(
                name: "exercise_step_id",
                table: "tests");

            migrationBuilder.AddColumn<string>(
                name: "contraints",
                table: "exercises",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "exercises",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "description_short",
                table: "exercises",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "hints",
                table: "exercises",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "parent_exercise_id",
                table: "exercises",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_exercises_parent_exercise_id",
                table: "exercises",
                column: "parent_exercise_id");

            migrationBuilder.AddForeignKey(
                name: "fk_exercises_exercises_parent_exercise_id",
                table: "exercises",
                column: "parent_exercise_id",
                principalTable: "exercises",
                principalColumn: "id");
        }
    }
}
