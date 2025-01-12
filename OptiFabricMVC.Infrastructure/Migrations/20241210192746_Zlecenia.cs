using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OptiFabricMVC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Zlecenia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_AspNetUsers_CurrentWorkerId",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Orders_OrderId",
                table: "Jobs");

            migrationBuilder.DropTable(
                name: "OrderProduct");

            migrationBuilder.DropTable(
                name: "ShiftApplicationUsers");

            migrationBuilder.DropTable(
                name: "WorkSessions");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_CurrentWorkerId",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_OrderId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "CompletedQuantity",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "CurrentWorkerId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "EmployeeComments",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "EstimatedTimePerUnit",
                table: "Jobs");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Jobs",
                newName: "TotalMissingQuantity");

            migrationBuilder.RenameColumn(
                name: "MissingQuantity",
                table: "Jobs",
                newName: "TotalCompletedQuantity");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Operation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstimatedTimePerUnit = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobOperation",
                columns: table => new
                {
                    JobsId = table.Column<int>(type: "int", nullable: false),
                    OperationsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobOperation", x => new { x.JobsId, x.OperationsId });
                    table.ForeignKey(
                        name: "FK_JobOperation_Jobs_JobsId",
                        column: x => x.JobsId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobOperation_Operation_OperationsId",
                        column: x => x.OperationsId,
                        principalTable: "Operation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_OrderId",
                table: "Products",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_JobOperation_OperationsId",
                table: "JobOperation",
                column: "OperationsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Orders_OrderId",
                table: "Products",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Orders_OrderId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "JobOperation");

            migrationBuilder.DropTable(
                name: "Operation");

            migrationBuilder.DropIndex(
                name: "IX_Products_OrderId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "TotalMissingQuantity",
                table: "Jobs",
                newName: "OrderId");

            migrationBuilder.RenameColumn(
                name: "TotalCompletedQuantity",
                table: "Jobs",
                newName: "MissingQuantity");

            migrationBuilder.AddColumn<int>(
                name: "CompletedQuantity",
                table: "Jobs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CurrentWorkerId",
                table: "Jobs",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeComments",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "EstimatedTimePerUnit",
                table: "Jobs",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.CreateTable(
                name: "OrderProduct",
                columns: table => new
                {
                    OrdersId = table.Column<int>(type: "int", nullable: false),
                    ProductsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProduct", x => new { x.OrdersId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_OrderProduct_Orders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProduct_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShiftApplicationUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftApplicationUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkSessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkSessions_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_CurrentWorkerId",
                table: "Jobs",
                column: "CurrentWorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_OrderId",
                table: "Jobs",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProduct_ProductsId",
                table: "OrderProduct",
                column: "ProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSessions_ApplicationUserId",
                table: "WorkSessions",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_AspNetUsers_CurrentWorkerId",
                table: "Jobs",
                column: "CurrentWorkerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Orders_OrderId",
                table: "Jobs",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
