﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OptiFabricMVC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Zlecenia3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobEmployees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeComments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompletedQuantity = table.Column<int>(type: "int", nullable: false),
                    MissingQuantity = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CurrentWorkerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    JobId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobEmployees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobEmployees_AspNetUsers_CurrentWorkerId",
                        column: x => x.CurrentWorkerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobEmployees_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobEmployees_CurrentWorkerId",
                table: "JobEmployees",
                column: "CurrentWorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_JobEmployees_JobId",
                table: "JobEmployees",
                column: "JobId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobEmployees");
        }
    }
}