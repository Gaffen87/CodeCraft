using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeCraftApi.Migrations
{
	/// <inheritdoc />
	public partial class newgroups : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "group_user");

			migrationBuilder.AlterDatabase()
				.Annotation("Npgsql:Enum:exercise_difficulty", "easy,hard,medium,unassigned")
				.Annotation("Npgsql:Enum:role", "student,teacher,unassigned")
				.Annotation("Npgsql:Enum:session_status", "active,passive,reconnecting")
				.Annotation("Npgsql:Enum:status", "active,passive")
				.OldAnnotation("Npgsql:Enum:exercise_difficulty", "easy,hard,medium,unassigned")
				.OldAnnotation("Npgsql:Enum:role", "student,teacher,unassigned")
				.OldAnnotation("Npgsql:Enum:session_status", "active,passive,reconnecting")
				.OldAnnotation("Npgsql:Enum:status", "active,passive");

			migrationBuilder.AddColumn<Guid>(
				name: "group_id",
				table: "users",
				type: "uuid",
				nullable: true);

			migrationBuilder.CreateIndex(
				name: "ix_users_group_id",
				table: "users",
				column: "group_id");

			migrationBuilder.AddForeignKey(
				name: "fk_users_groups_group_id",
				table: "users",
				column: "group_id",
				principalTable: "groups",
				principalColumn: "id");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "fk_users_groups_group_id",
				table: "users");

			migrationBuilder.DropIndex(
				name: "ix_users_group_id",
				table: "users");

			migrationBuilder.DropColumn(
				name: "group_id",
				table: "users");

			migrationBuilder.AlterDatabase()
				.Annotation("Npgsql:Enum:exercise_difficulty", "easy,hard,medium,unassigned")
				.Annotation("Npgsql:Enum:group_size", "group,pair,team")
				.Annotation("Npgsql:Enum:role", "student,teacher,unassigned")
				.Annotation("Npgsql:Enum:session_status", "active,passive,reconnecting")
				.Annotation("Npgsql:Enum:status", "active,passive")
				.OldAnnotation("Npgsql:Enum:exercise_difficulty", "easy,hard,medium,unassigned")
				.OldAnnotation("Npgsql:Enum:role", "student,teacher,unassigned")
				.OldAnnotation("Npgsql:Enum:session_status", "active,passive,reconnecting")
				.OldAnnotation("Npgsql:Enum:status", "active,passive");

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

			migrationBuilder.CreateIndex(
				name: "ix_group_user_members_id",
				table: "group_user",
				column: "members_id");
		}
	}
}
