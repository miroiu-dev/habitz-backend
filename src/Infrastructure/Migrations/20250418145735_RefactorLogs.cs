using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations;

/// <inheritdoc />
public partial class RefactorLogs : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "fk_body_composition_log_users_user_id",
            schema: "public",
            table: "body_composition_log");

        migrationBuilder.DropPrimaryKey(
            name: "pk_body_composition_log",
            schema: "public",
            table: "body_composition_log");

        migrationBuilder.RenameTable(
            name: "body_composition_log",
            schema: "public",
            newName: "body_composition_logs",
            newSchema: "public");

        migrationBuilder.RenameColumn(
            name: "create_at",
            schema: "public",
            table: "body_composition_logs",
            newName: "created_at");

        migrationBuilder.RenameIndex(
            name: "ix_body_composition_log_user_id",
            schema: "public",
            table: "body_composition_logs",
            newName: "ix_body_composition_logs_user_id");

        migrationBuilder.AddColumn<decimal>(
            name: "waist_hip_ratio",
            schema: "public",
            table: "body_composition_logs",
            type: "numeric",
            nullable: false,
            defaultValue: 0m);

        migrationBuilder.AddPrimaryKey(
            name: "pk_body_composition_logs",
            schema: "public",
            table: "body_composition_logs",
            column: "id");

        migrationBuilder.AddForeignKey(
            name: "fk_body_composition_logs_users_user_id",
            schema: "public",
            table: "body_composition_logs",
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
            name: "fk_body_composition_logs_users_user_id",
            schema: "public",
            table: "body_composition_logs");

        migrationBuilder.DropPrimaryKey(
            name: "pk_body_composition_logs",
            schema: "public",
            table: "body_composition_logs");

        migrationBuilder.DropColumn(
            name: "waist_hip_ratio",
            schema: "public",
            table: "body_composition_logs");

        migrationBuilder.RenameTable(
            name: "body_composition_logs",
            schema: "public",
            newName: "body_composition_log",
            newSchema: "public");

        migrationBuilder.RenameColumn(
            name: "created_at",
            schema: "public",
            table: "body_composition_log",
            newName: "create_at");

        migrationBuilder.RenameIndex(
            name: "ix_body_composition_logs_user_id",
            schema: "public",
            table: "body_composition_log",
            newName: "ix_body_composition_log_user_id");

        migrationBuilder.AddPrimaryKey(
            name: "pk_body_composition_log",
            schema: "public",
            table: "body_composition_log",
            column: "id");

        migrationBuilder.AddForeignKey(
            name: "fk_body_composition_log_users_user_id",
            schema: "public",
            table: "body_composition_log",
            column: "user_id",
            principalSchema: "public",
            principalTable: "users",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);
    }
}
