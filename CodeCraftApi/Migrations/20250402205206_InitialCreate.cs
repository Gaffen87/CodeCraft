using CodeCraftApi.Domain.Entities;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeCraftApi.Migrations
{
	/// <inheritdoc />
	public partial class InitialCreate : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterDatabase()
				.Annotation("Npgsql:Enum:exercise_difficulty", "easy,hard,medium,unassigned")
				.Annotation("Npgsql:Enum:group_size", "group,pair,team")
				.Annotation("Npgsql:Enum:role", "student,teacher,unassigned")
				.Annotation("Npgsql:Enum:session_status", "active,passive,reconnecting")
				.Annotation("Npgsql:Enum:status", "active,passive");

			migrationBuilder.CreateTable(
				name: "categories",
				columns: table => new
				{
					id = table.Column<Guid>(type: "uuid", nullable: false),
					name = table.Column<string>(type: "text", nullable: false),
					description = table.Column<string>(type: "text", nullable: false),
					created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("pk_categories", x => x.id);
				});

			migrationBuilder.CreateTable(
				name: "groups",
				columns: table => new
				{
					id = table.Column<Guid>(type: "uuid", nullable: false),
					name = table.Column<string>(type: "text", nullable: false),
					created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
					updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
					is_active = table.Column<bool>(type: "boolean", nullable: false),
					is_deleted = table.Column<bool>(type: "boolean", nullable: false),
				},
				constraints: table =>
				{
					table.PrimaryKey("pk_groups", x => x.id);
				});

			migrationBuilder.CreateTable(
				name: "tests",
				columns: table => new
				{
					id = table.Column<Guid>(type: "uuid", nullable: false),
					content = table.Column<string>(type: "json", nullable: false),
					created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
					updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("pk_tests", x => x.id);
				});

			migrationBuilder.CreateTable(
				name: "users",
				columns: table => new
				{
					id = table.Column<Guid>(type: "uuid", nullable: false),
					role = table.Column<Role>(type: "role", nullable: false),
					status = table.Column<Status>(type: "status", nullable: false),
					email = table.Column<string>(type: "text", nullable: false),
					user_name = table.Column<string>(type: "text", nullable: false),
					created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
					updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
					is_deleted = table.Column<bool>(type: "boolean", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("pk_users", x => x.id);
				});

			migrationBuilder.CreateTable(
				name: "exercises",
				columns: table => new
				{
					id = table.Column<Guid>(type: "uuid", nullable: false),
					parent_exercise_id = table.Column<Guid>(type: "uuid", nullable: true),
					author_id = table.Column<Guid>(type: "uuid", nullable: false),
					exercise_difficulty = table.Column<ExerciseDifficulty>(type: "exercise_difficulty", nullable: false),
					title = table.Column<string>(type: "text", nullable: false),
					description = table.Column<string>(type: "text", nullable: false),
					description_short = table.Column<string>(type: "text", nullable: false),
					contraints = table.Column<string>(type: "text", nullable: false),
					hints = table.Column<string>(type: "text", nullable: false),
					created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
					updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
					is_deleted = table.Column<bool>(type: "boolean", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("pk_exercises", x => x.id);
					table.ForeignKey(
						name: "fk_exercises_exercises_parent_exercise_id",
						column: x => x.parent_exercise_id,
						principalTable: "exercises",
						principalColumn: "id");
					table.ForeignKey(
						name: "fk_exercises_users_author_id",
						column: x => x.author_id,
						principalTable: "users",
						principalColumn: "id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "group_user",
				columns: table => new
				{
					groups_id = table.Column<Guid>(type: "uuid", nullable: false),
					members_id = table.Column<Guid>(type: "uuid", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("pk_group_user", x => new { x.groups_id, x.members_id });
					table.ForeignKey(
						name: "fk_group_user_groups_groups_id",
						column: x => x.groups_id,
						principalTable: "groups",
						principalColumn: "id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "fk_group_user_users_members_id",
						column: x => x.members_id,
						principalTable: "users",
						principalColumn: "id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "sessions",
				columns: table => new
				{
					id = table.Column<Guid>(type: "uuid", nullable: false),
					name = table.Column<string>(type: "text", nullable: false),
					session_status = table.Column<SessionStatus>(type: "session_status", nullable: false),
					user_id = table.Column<Guid>(type: "uuid", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("pk_sessions", x => x.id);
					table.ForeignKey(
						name: "fk_sessions_users_user_id",
						column: x => x.user_id,
						principalTable: "users",
						principalColumn: "id");
				});

			migrationBuilder.CreateTable(
				name: "category_exercise",
				columns: table => new
				{
					categories_id = table.Column<Guid>(type: "uuid", nullable: false),
					exercises_id = table.Column<Guid>(type: "uuid", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("pk_category_exercise", x => new { x.categories_id, x.exercises_id });
					table.ForeignKey(
						name: "fk_category_exercise_categories_categories_id",
						column: x => x.categories_id,
						principalTable: "categories",
						principalColumn: "id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "fk_category_exercise_exercises_exercises_id",
						column: x => x.exercises_id,
						principalTable: "exercises",
						principalColumn: "id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "group_exercise",
				columns: table => new
				{
					exercises_id = table.Column<Guid>(type: "uuid", nullable: false),
					groups_id = table.Column<Guid>(type: "uuid", nullable: false),
					is_visible = table.Column<bool>(type: "boolean", nullable: false),
					updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("pk_group_exercise", x => new { x.exercises_id, x.groups_id });
					table.ForeignKey(
						name: "fk_group_exercise_exercises_exercises_id",
						column: x => x.exercises_id,
						principalTable: "exercises",
						principalColumn: "id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "fk_group_exercise_groups_groups_id",
						column: x => x.groups_id,
						principalTable: "groups",
						principalColumn: "id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "ix_category_exercise_exercises_id",
				table: "category_exercise",
				column: "exercises_id");

			migrationBuilder.CreateIndex(
				name: "ix_exercises_author_id",
				table: "exercises",
				column: "author_id");

			migrationBuilder.CreateIndex(
				name: "ix_exercises_parent_exercise_id",
				table: "exercises",
				column: "parent_exercise_id");

			migrationBuilder.CreateIndex(
				name: "ix_group_exercise_groups_id",
				table: "group_exercise",
				column: "groups_id");

			migrationBuilder.CreateIndex(
				name: "ix_group_user_members_id",
				table: "group_user",
				column: "members_id");

			migrationBuilder.CreateIndex(
				name: "ix_sessions_user_id",
				table: "sessions",
				column: "user_id");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "category_exercise");

			migrationBuilder.DropTable(
				name: "group_exercise");

			migrationBuilder.DropTable(
				name: "group_user");

			migrationBuilder.DropTable(
				name: "sessions");

			migrationBuilder.DropTable(
				name: "tests");

			migrationBuilder.DropTable(
				name: "categories");

			migrationBuilder.DropTable(
				name: "exercises");

			migrationBuilder.DropTable(
				name: "groups");

			migrationBuilder.DropTable(
				name: "users");
		}
	}
}
