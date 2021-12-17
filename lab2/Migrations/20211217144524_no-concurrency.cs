using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace lab1.Migrations
{
    public partial class noconcurrency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    user_name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.user_name);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "activities",
                columns: table => new
                {
                    activity_code = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    activity_name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    budget = table.Column<int>(type: "int", nullable: false),
                    active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    manager_name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_activities", x => x.activity_code);
                    table.ForeignKey(
                        name: "fk_activities_users_manager_name",
                        column: x => x.manager_name,
                        principalTable: "users",
                        principalColumn: "user_name",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "reports",
                columns: table => new
                {
                    report_month = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    user_name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    frozen = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_reports", x => new { x.report_month, x.user_name });
                    table.ForeignKey(
                        name: "fk_reports_users_user_name",
                        column: x => x.user_name,
                        principalTable: "users",
                        principalColumn: "user_name",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "subcodes",
                columns: table => new
                {
                    subactivity_code = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    activity_code = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_subcodes", x => new { x.activity_code, x.subactivity_code });
                    table.ForeignKey(
                        name: "fk_subcodes_activities_activity_code1",
                        column: x => x.activity_code,
                        principalTable: "activities",
                        principalColumn: "activity_code",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "accepted_entries",
                columns: table => new
                {
                    user_name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    activity_code = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    report_month = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    time = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_accepted_entries", x => new { x.report_month, x.user_name, x.activity_code });
                    table.ForeignKey(
                        name: "fk_accepted_entries_activities_activity_code",
                        column: x => x.activity_code,
                        principalTable: "activities",
                        principalColumn: "activity_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_accepted_entries_reports_report_month_user_name",
                        columns: x => new { x.report_month, x.user_name },
                        principalTable: "reports",
                        principalColumns: new[] { "report_month", "user_name" },
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "entries",
                columns: table => new
                {
                    entry_pid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    user_name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    report_month = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    activity_code = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    time = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    subactivity_code = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_entries", x => new { x.report_month, x.user_name, x.activity_code, x.entry_pid });
                    table.ForeignKey(
                        name: "fk_entries_activities_activity_code1",
                        column: x => x.activity_code,
                        principalTable: "activities",
                        principalColumn: "activity_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_entries_reports_report_month_user_name",
                        columns: x => new { x.report_month, x.user_name },
                        principalTable: "reports",
                        principalColumns: new[] { "report_month", "user_name" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_entries_subcodes_activity_code_subactivity_code",
                        columns: x => new { x.activity_code, x.subactivity_code },
                        principalTable: "subcodes",
                        principalColumns: new[] { "activity_code", "subactivity_code" },
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_accepted_entries_activity_code",
                table: "accepted_entries",
                column: "activity_code");

            migrationBuilder.CreateIndex(
                name: "ix_activities_manager_name",
                table: "activities",
                column: "manager_name");

            migrationBuilder.CreateIndex(
                name: "ix_entries_activity_code_subactivity_code",
                table: "entries",
                columns: new[] { "activity_code", "subactivity_code" });

            migrationBuilder.CreateIndex(
                name: "ix_reports_user_name",
                table: "reports",
                column: "user_name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "accepted_entries");

            migrationBuilder.DropTable(
                name: "entries");

            migrationBuilder.DropTable(
                name: "reports");

            migrationBuilder.DropTable(
                name: "subcodes");

            migrationBuilder.DropTable(
                name: "activities");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
