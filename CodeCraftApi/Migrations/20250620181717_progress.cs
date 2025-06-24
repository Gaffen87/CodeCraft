using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeCraftApi.Migrations
{
    /// <inheritdoc />
    public partial class progress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_exercise_progress");

            migrationBuilder.DropTable(
                name: "user_step_progress");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:exercise_difficulty", "easy,hard,medium,unassigned")
                .Annotation("Npgsql:Enum:role", "student,teacher,unassigned")
                .Annotation("Npgsql:Enum:session_status", "active,passive,reconnecting")
                .Annotation("Npgsql:Enum:status", "active,passive")
                .OldAnnotation("Npgsql:Enum:exercise_difficulty", "easy,hard,medium,unassigned")
                .OldAnnotation("Npgsql:Enum:progress", "completed,not_started,ongoing")
                .OldAnnotation("Npgsql:Enum:role", "student,teacher,unassigned")
                .OldAnnotation("Npgsql:Enum:session_status", "active,passive,reconnecting")
                .OldAnnotation("Npgsql:Enum:status", "active,passive");

            migrationBuilder.AddColumn<Guid>(
                name: "exercise_step_id",
                table: "users",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "exercise_progress",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    exercise_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    progress = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_exercise_progress", x => x.id);
                    table.ForeignKey(
                        name: "fk_exercise_progress_exercises_exercise_id",
                        column: x => x.exercise_id,
                        principalTable: "exercises",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_exercise_progress_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "step_progress",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    exercise_step_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    completed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_step_progress", x => x.id);
                    table.ForeignKey(
                        name: "fk_step_progress_exercise_step_exercise_step_id",
                        column: x => x.exercise_step_id,
                        principalTable: "exercise_step",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_step_progress_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_users_exercise_step_id",
                table: "users",
                column: "exercise_step_id");

            migrationBuilder.CreateIndex(
                name: "ix_exercise_progress_exercise_id",
                table: "exercise_progress",
                column: "exercise_id");

            migrationBuilder.CreateIndex(
                name: "ix_exercise_progress_user_id",
                table: "exercise_progress",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_step_progress_exercise_step_id",
                table: "step_progress",
                column: "exercise_step_id");

            migrationBuilder.CreateIndex(
                name: "ix_step_progress_user_id",
                table: "step_progress",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_users_exercise_step_exercise_step_id",
                table: "users",
                column: "exercise_step_id",
                principalTable: "exercise_step",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_users_exercise_step_exercise_step_id",
                table: "users");

            migrationBuilder.DropTable(
                name: "exercise_progress");

            migrationBuilder.DropTable(
                name: "step_progress");

            migrationBuilder.DropIndex(
                name: "ix_users_exercise_step_id",
                table: "users");

            migrationBuilder.DropColumn(
                name: "exercise_step_id",
                table: "users");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:exercise_difficulty", "easy,hard,medium,unassigned")
                .Annotation("Npgsql:Enum:progress", "completed,not_started,ongoing")
                .Annotation("Npgsql:Enum:role", "student,teacher,unassigned")
                .Annotation("Npgsql:Enum:session_status", "active,passive,reconnecting")
                .Annotation("Npgsql:Enum:status", "active,passive")
                .OldAnnotation("Npgsql:Enum:exercise_difficulty", "easy,hard,medium,unassigned")
                .OldAnnotation("Npgsql:Enum:role", "student,teacher,unassigned")
                .OldAnnotation("Npgsql:Enum:session_status", "active,passive,reconnecting")
                .OldAnnotation("Npgsql:Enum:status", "active,passive");

            migrationBuilder.CreateTable(
                name: "user_exercise_progress",
                columns: table => new
                {
                    exercise_progress_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_progress_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_exercise_progress", x => new { x.exercise_progress_id, x.user_progress_id });
                    table.ForeignKey(
                        name: "fk_user_exercise_progress_exercises_exercise_progress_id",
                        column: x => x.exercise_progress_id,
                        principalTable: "exercises",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_exercise_progress_users_user_progress_id",
                        column: x => x.user_progress_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_step_progress",
                columns: table => new
                {
                    step_progress_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_progress_id = table.Column<Guid>(type: "uuid", nullable: false),
                    completed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_step_progress", x => new { x.step_progress_id, x.user_progress_id });
                    table.ForeignKey(
                        name: "fk_user_step_progress_exercise_step_step_progress_id",
                        column: x => x.step_progress_id,
                        principalTable: "exercise_step",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_step_progress_users_user_progress_id",
                        column: x => x.user_progress_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_user_exercise_progress_user_progress_id",
                table: "user_exercise_progress",
                column: "user_progress_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_step_progress_user_progress_id",
                table: "user_step_progress",
                column: "user_progress_id");
        }
    }
}
