using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations;

/// <inheritdoc />
public partial class BodyCompositionLogs : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "body_composition_log",
            schema: "public",
            columns: table => new
            {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                create_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                neck = table.Column<decimal>(type: "numeric", nullable: false),
                left_biceps = table.Column<decimal>(type: "numeric", nullable: false),
                right_biceps = table.Column<decimal>(type: "numeric", nullable: false),
                chest = table.Column<decimal>(type: "numeric", nullable: false),
                abs = table.Column<decimal>(type: "numeric", nullable: false),
                left_tigh = table.Column<decimal>(type: "numeric", nullable: false),
                right_tigh = table.Column<decimal>(type: "numeric", nullable: false),
                left_calf = table.Column<decimal>(type: "numeric", nullable: false),
                right_calf = table.Column<decimal>(type: "numeric", nullable: false),
                shoulder = table.Column<decimal>(type: "numeric", nullable: false),
                waist = table.Column<decimal>(type: "numeric", nullable: false),
                hip = table.Column<decimal>(type: "numeric", nullable: false),
                user_id = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_body_composition_log", x => x.id);
                table.ForeignKey(
                    name: "fk_body_composition_log_users_user_id",
                    column: x => x.user_id,
                    principalSchema: "public",
                    principalTable: "users",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "ix_body_composition_log_user_id",
            schema: "public",
            table: "body_composition_log",
            column: "user_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "body_composition_log",
            schema: "public");
    }
}
