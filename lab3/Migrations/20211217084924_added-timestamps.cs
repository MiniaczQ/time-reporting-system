using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace lab1.Migrations
{
    public partial class addedtimestamps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "timestamp",
                table: "entries",
                type: "bytea",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "timestamp",
                table: "accepted_entries",
                type: "bytea",
                rowVersion: true,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "timestamp",
                table: "entries");

            migrationBuilder.DropColumn(
                name: "timestamp",
                table: "accepted_entries");
        }
    }
}
