using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RL.Data.Migrations
{
    public partial class SelectedList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProcedureUsers_PlanProcedures_PlanProcedurePlanId_PlanProcedureProcedureId",
                table: "ProcedureUsers");

            migrationBuilder.DropIndex(
                name: "IX_ProcedureUsers_PlanProcedurePlanId_PlanProcedureProcedureId",
                table: "ProcedureUsers");

            migrationBuilder.DropColumn(
                name: "PlanProcedurePlanId",
                table: "ProcedureUsers");

            migrationBuilder.DropColumn(
                name: "PlanProcedureProcedureId",
                table: "ProcedureUsers");

            migrationBuilder.CreateTable(
                name: "SelectedList",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProcedureId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlanId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsChecked = table.Column<bool>(type: "INTEGER", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelectedList", x => new { x.UserId, x.ProcedureId, x.PlanId });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SelectedList");

            migrationBuilder.AddColumn<int>(
                name: "PlanProcedurePlanId",
                table: "ProcedureUsers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlanProcedureProcedureId",
                table: "ProcedureUsers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProcedureUsers_PlanProcedurePlanId_PlanProcedureProcedureId",
                table: "ProcedureUsers",
                columns: new[] { "PlanProcedurePlanId", "PlanProcedureProcedureId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProcedureUsers_PlanProcedures_PlanProcedurePlanId_PlanProcedureProcedureId",
                table: "ProcedureUsers",
                columns: new[] { "PlanProcedurePlanId", "PlanProcedureProcedureId" },
                principalTable: "PlanProcedures",
                principalColumns: new[] { "PlanId", "ProcedureId" });
        }
    }
}
