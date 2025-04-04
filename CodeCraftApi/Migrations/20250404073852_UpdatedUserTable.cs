using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeCraftApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_exercises_users_author_id",
                table: "exercises");

            migrationBuilder.DropColumn(
                name: "contraints",
                table: "exercise_item");

            migrationBuilder.DropColumn(
                name: "description",
                table: "exercise_item");

            migrationBuilder.DropColumn(
                name: "description_short",
                table: "exercise_item");

            migrationBuilder.DropColumn(
                name: "hints",
                table: "exercise_item");

            migrationBuilder.AlterColumn<Guid>(
                name: "author_id",
                table: "exercises",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<string>(
                name: "summary",
                table: "exercises",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "fk_exercises_users_author_id",
                table: "exercises",
                column: "author_id",
                principalTable: "users",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_exercises_users_author_id",
                table: "exercises");

            migrationBuilder.DropColumn(
                name: "summary",
                table: "exercises");

            migrationBuilder.AlterColumn<Guid>(
                name: "author_id",
                table: "exercises",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "contraints",
                table: "exercise_item",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "exercise_item",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "description_short",
                table: "exercise_item",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "hints",
                table: "exercise_item",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "fk_exercises_users_author_id",
                table: "exercises",
                column: "author_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
