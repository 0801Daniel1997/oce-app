using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RL.Data.Migrations
{
    public partial class InitialCreate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProcedureUsers",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProcedureId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlanId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PlanProcedurePlanId = table.Column<int>(type: "INTEGER", nullable: true),
                    PlanProcedureProcedureId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcedureUsers", x => new { x.UserId, x.ProcedureId, x.PlanId });
                    table.ForeignKey(
                        name: "FK_ProcedureUsers_PlanProcedures_PlanProcedurePlanId_PlanProcedureProcedureId",
                        columns: x => new { x.PlanProcedurePlanId, x.PlanProcedureProcedureId },
                        principalTable: "PlanProcedures",
                        principalColumns: new[] { "PlanId", "ProcedureId" });
                    table.ForeignKey(
                        name: "FK_ProcedureUsers_Procedures_ProcedureId",
                        column: x => x.ProcedureId,
                        principalTable: "Procedures",
                        principalColumn: "ProcedureId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProcedureUsers_PlanProcedurePlanId_PlanProcedureProcedureId",
                table: "ProcedureUsers",
                columns: new[] { "PlanProcedurePlanId", "PlanProcedureProcedureId" });

            migrationBuilder.CreateIndex(
                name: "IX_ProcedureUsers_ProcedureId",
                table: "ProcedureUsers",
                column: "ProcedureId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProcedureUsers");
        }
    }
}
