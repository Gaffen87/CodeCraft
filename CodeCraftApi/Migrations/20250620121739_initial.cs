using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeCraftApi.Migrations
{
	/// <inheritdoc />
	public partial class initial : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{






			migrationBuilder.CreateTable(
				name: "user_exercise_progress",
				columns: table => new
				{
					exercise_progress_id = table.Column<Guid>(type: "uuid", nullable: false),
					user_progress_id = table.Column<Guid>(type: "uuid", nullable: false),
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

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "category_exercise");

			migrationBuilder.DropTable(
				name: "code_files");

			migrationBuilder.DropTable(
				name: "group_exercise");

			migrationBuilder.DropTable(
				name: "sessions");

			migrationBuilder.DropTable(
				name: "tests");

			migrationBuilder.DropTable(
				name: "user_exercise_progress");

			migrationBuilder.DropTable(
				name: "user_step_progress");

			migrationBuilder.DropTable(
				name: "categories");

			migrationBuilder.DropTable(
				name: "submissions");

			migrationBuilder.DropTable(
				name: "users");

			migrationBuilder.DropTable(
				name: "exercise_step");

			migrationBuilder.DropTable(
				name: "groups");

			migrationBuilder.DropTable(
				name: "exercise_item");

			migrationBuilder.DropTable(
				name: "exercises");
		}
	}
}
