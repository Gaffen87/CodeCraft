using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeCraftApi.Migrations
{
	/// <inheritdoc />
	public partial class refactortablesubmissions : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "content",
				table: "submissions");

			migrationBuilder.AlterColumn<string>(
				name: "result",
				table: "submissions",
				type: "text",
				nullable: false,
				oldClrType: typeof(string),
				oldType: "jsonb");

			migrationBuilder.CreateTable(
				name: "code_files",
				columns: table => new
				{
					id = table.Column<Guid>(type: "uuid", nullable: false),
					file_name = table.Column<string>(type: "text", nullable: false),
					content = table.Column<string>(type: "text", nullable: false),
					code_submission_id = table.Column<Guid>(type: "uuid", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("pk_code_files", x => x.id);
					table.ForeignKey(
						name: "fk_code_files_submissions_code_submission_id",
						column: x => x.code_submission_id,
						principalTable: "submissions",
						principalColumn: "id");
				});

			migrationBuilder.CreateIndex(
				name: "ix_code_files_code_submission_id",
				table: "code_files",
				column: "code_submission_id");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "code_files");

			migrationBuilder.AlterColumn<string>(
				name: "result",
				table: "submissions",
				type: "jsonb",
				nullable: false,
				oldClrType: typeof(string),
				oldType: "text");

			migrationBuilder.AddColumn<string>(
				name: "content",
				table: "submissions",
				type: "jsonb",
				nullable: false,
				defaultValue: "");
		}
	}
}
