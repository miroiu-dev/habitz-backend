using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations;

/// <inheritdoc />
public partial class HabitLogs : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "fk_habit_users_user_id",
            schema: "public",
            table: "habit");

        migrationBuilder.DropForeignKey(
            name: "fk_habit_schedule_habit_habit_id",
            schema: "public",
            table: "habit_schedule");

        migrationBuilder.DropPrimaryKey(
            name: "pk_habit_schedule",
            schema: "public",
            table: "habit_schedule");

        migrationBuilder.DropPrimaryKey(
            name: "pk_habit",
            schema: "public",
            table: "habit");

        migrationBuilder.RenameTable(
            name: "habit_schedule",
            schema: "public",
            newName: "habit_schedules",
            newSchema: "public");

        migrationBuilder.RenameTable(
            name: "habit",
            schema: "public",
            newName: "habits",
            newSchema: "public");

        migrationBuilder.RenameIndex(
            name: "ix_habit_schedule_habit_id",
            schema: "public",
            table: "habit_schedules",
            newName: "ix_habit_schedules_habit_id");

        migrationBuilder.RenameIndex(
            name: "ix_habit_user_id",
            schema: "public",
            table: "habits",
            newName: "ix_habits_user_id");

        migrationBuilder.AlterColumn<int>(
            name: "day_of_week",
            schema: "public",
            table: "habit_schedules",
            type: "integer",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "text");

        migrationBuilder.AddPrimaryKey(
            name: "pk_habit_schedules",
            schema: "public",
            table: "habit_schedules",
            column: "id");

        migrationBuilder.AddPrimaryKey(
            name: "pk_habits",
            schema: "public",
            table: "habits",
            column: "id");

        migrationBuilder.CreateTable(
            name: "habit_logs",
            schema: "public",
            columns: table => new
            {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                habit_id = table.Column<int>(type: "integer", nullable: false),
                created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                is_completed = table.Column<bool>(type: "boolean", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_habit_logs", x => x.id);
                table.ForeignKey(
                    name: "fk_habit_logs_habits_habit_id",
                    column: x => x.habit_id,
                    principalSchema: "public",
                    principalTable: "habits",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "ix_habit_logs_habit_id",
            schema: "public",
            table: "habit_logs",
            column: "habit_id");

        migrationBuilder.AddForeignKey(
            name: "fk_habit_schedules_habits_habit_id",
            schema: "public",
            table: "habit_schedules",
            column: "habit_id",
            principalSchema: "public",
            principalTable: "habits",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "fk_habits_users_user_id",
            schema: "public",
            table: "habits",
            column: "user_id",
            principalSchema: "public",
            principalTable: "users",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "fk_habit_schedules_habits_habit_id",
            schema: "public",
            table: "habit_schedules");

        migrationBuilder.DropForeignKey(
            name: "fk_habits_users_user_id",
            schema: "public",
            table: "habits");

        migrationBuilder.DropTable(
            name: "habit_logs",
            schema: "public");

        migrationBuilder.DropPrimaryKey(
            name: "pk_habits",
            schema: "public",
            table: "habits");

        migrationBuilder.DropPrimaryKey(
            name: "pk_habit_schedules",
            schema: "public",
            table: "habit_schedules");

        migrationBuilder.RenameTable(
            name: "habits",
            schema: "public",
            newName: "habit",
            newSchema: "public");

        migrationBuilder.RenameTable(
            name: "habit_schedules",
            schema: "public",
            newName: "habit_schedule",
            newSchema: "public");

        migrationBuilder.RenameIndex(
            name: "ix_habits_user_id",
            schema: "public",
            table: "habit",
            newName: "ix_habit_user_id");

        migrationBuilder.RenameIndex(
            name: "ix_habit_schedules_habit_id",
            schema: "public",
            table: "habit_schedule",
            newName: "ix_habit_schedule_habit_id");

        migrationBuilder.AlterColumn<string>(
            name: "day_of_week",
            schema: "public",
            table: "habit_schedule",
            type: "text",
            nullable: false,
            oldClrType: typeof(int),
            oldType: "integer");

        migrationBuilder.AddPrimaryKey(
            name: "pk_habit",
            schema: "public",
            table: "habit",
            column: "id");

        migrationBuilder.AddPrimaryKey(
            name: "pk_habit_schedule",
            schema: "public",
            table: "habit_schedule",
            column: "id");

        migrationBuilder.AddForeignKey(
            name: "fk_habit_users_user_id",
            schema: "public",
            table: "habit",
            column: "user_id",
            principalSchema: "public",
            principalTable: "users",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "fk_habit_schedule_habit_habit_id",
            schema: "public",
            table: "habit_schedule",
            column: "habit_id",
            principalSchema: "public",
            principalTable: "habit",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);
    }
}
