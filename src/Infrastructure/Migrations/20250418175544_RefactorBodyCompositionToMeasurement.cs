using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations;

/// <inheritdoc />
public partial class RefactorBodyCompositionToMeasurement : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "body_composition_logs",
            schema: "public");

        migrationBuilder.CreateTable(
            name: "body_measurement_logs",
            schema: "public",
            columns: table => new
            {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
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
                user_id = table.Column<int>(type: "integer", nullable: false),
                waist_hip_ratio = table.Column<decimal>(type: "numeric", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_body_measurement_logs", x => x.id);
                table.ForeignKey(
                    name: "fk_body_measurement_logs_users_user_id",
                    column: x => x.user_id,
                    principalSchema: "public",
                    principalTable: "users",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "ix_body_measurement_logs_user_id",
            schema: "public",
            table: "body_measurement_logs",
            column: "user_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "body_measurement_logs",
            schema: "public");

        migrationBuilder.CreateTable(
            name: "body_composition_logs",
            schema: "public",
            columns: table => new
            {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                user_id = table.Column<int>(type: "integer", nullable: false),
                abs = table.Column<decimal>(type: "numeric", nullable: false),
                chest = table.Column<decimal>(type: "numeric", nullable: false),
                created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                hip = table.Column<decimal>(type: "numeric", nullable: false),
                left_biceps = table.Column<decimal>(type: "numeric", nullable: false),
                left_calf = table.Column<decimal>(type: "numeric", nullable: false),
                left_tigh = table.Column<decimal>(type: "numeric", nullable: false),
                neck = table.Column<decimal>(type: "numeric", nullable: false),
                right_biceps = table.Column<decimal>(type: "numeric", nullable: false),
                right_calf = table.Column<decimal>(type: "numeric", nullable: false),
                right_tigh = table.Column<decimal>(type: "numeric", nullable: false),
                shoulder = table.Column<decimal>(type: "numeric", nullable: false),
                waist = table.Column<decimal>(type: "numeric", nullable: false),
                waist_hip_ratio = table.Column<decimal>(type: "numeric", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_body_composition_logs", x => x.id);
                table.ForeignKey(
                    name: "fk_body_composition_logs_users_user_id",
                    column: x => x.user_id,
                    principalSchema: "public",
                    principalTable: "users",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "ix_body_composition_logs_user_id",
            schema: "public",
            table: "body_composition_logs",
            column: "user_id");
    }
}
