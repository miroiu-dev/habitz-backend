﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations;

/// <inheritdoc />
public partial class NullableWEEKLYGOAL : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<decimal>(
            name: "weekly_goal",
            schema: "public",
            table: "users",
            type: "numeric",
            nullable: true,
            oldClrType: typeof(decimal),
            oldType: "numeric");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<decimal>(
            name: "weekly_goal",
            schema: "public",
            table: "users",
            type: "numeric",
            nullable: false,
            defaultValue: 0m,
            oldClrType: typeof(decimal),
            oldType: "numeric",
            oldNullable: true);
    }
}
