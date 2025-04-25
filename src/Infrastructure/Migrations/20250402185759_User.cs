using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations;

/// <inheritdoc />
public partial class User : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "public");

        migrationBuilder.CreateTable(
            name: "users",
            schema: "public",
            columns: table => new
            {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                email = table.Column<string>(type: "text", nullable: false),
                full_name = table.Column<string>(type: "text", nullable: false),
                age = table.Column<int>(type: "integer", nullable: false),
                activity_level = table.Column<int>(type: "integer", nullable: false),
                gender = table.Column<int>(type: "integer", nullable: false),
                goal = table.Column<int>(type: "integer", nullable: false),
                goal_weight = table.Column<int>(type: "integer", nullable: true),
                height = table.Column<int>(type: "integer", nullable: false),
                weekly_goal = table.Column<decimal>(type: "numeric", nullable: false),
                weight = table.Column<int>(type: "integer", nullable: false),
                password_hash = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table => table.PrimaryKey("pk_users", x => x.id));

        migrationBuilder.CreateTable(
            name: "habit",
            schema: "public",
            columns: table => new
            {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                name = table.Column<string>(type: "text", nullable: false),
                icon = table.Column<string>(type: "text", nullable: false),
                color = table.Column<string>(type: "text", nullable: false),
                user_id = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_habit", x => x.id);
                table.ForeignKey(
                    name: "fk_habit_users_user_id",
                    column: x => x.user_id,
                    principalSchema: "public",
                    principalTable: "users",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "refresh_tokens",
            schema: "public",
            columns: table => new
            {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                token = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                user_id = table.Column<int>(type: "integer", nullable: false),
                expires_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_refresh_tokens", x => x.id);
                table.ForeignKey(
                    name: "fk_refresh_tokens_users_user_id",
                    column: x => x.user_id,
                    principalSchema: "public",
                    principalTable: "users",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "habit_schedule",
            schema: "public",
            columns: table => new
            {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                day_of_week = table.Column<string>(type: "text", nullable: false),
                habit_id = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_habit_schedule", x => x.id);
                table.ForeignKey(
                    name: "fk_habit_schedule_habit_habit_id",
                    column: x => x.habit_id,
                    principalSchema: "public",
                    principalTable: "habit",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "ix_habit_user_id",
            schema: "public",
            table: "habit",
            column: "user_id");

        migrationBuilder.CreateIndex(
            name: "ix_habit_schedule_habit_id",
            schema: "public",
            table: "habit_schedule",
            column: "habit_id");

        migrationBuilder.CreateIndex(
            name: "ix_refresh_tokens_token",
            schema: "public",
            table: "refresh_tokens",
            column: "token",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "ix_refresh_tokens_user_id",
            schema: "public",
            table: "refresh_tokens",
            column: "user_id");

        migrationBuilder.CreateIndex(
            name: "ix_users_email",
            schema: "public",
            table: "users",
            column: "email",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "habit_schedule",
            schema: "public");

        migrationBuilder.DropTable(
            name: "refresh_tokens",
            schema: "public");

        migrationBuilder.DropTable(
            name: "habit",
            schema: "public");

        migrationBuilder.DropTable(
            name: "users",
            schema: "public");
    }
}
