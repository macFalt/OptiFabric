using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OptiFabricMVC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ZmianaApplicationUser12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShiftApplicationUsers_AspNetUsers_ApplicationUserId1",
                table: "ShiftApplicationUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ShiftApplicationUsers_Shifts_ShiftId",
                table: "ShiftApplicationUsers");

            migrationBuilder.DropIndex(
                name: "IX_ShiftApplicationUsers_ApplicationUserId1",
                table: "ShiftApplicationUsers");

            migrationBuilder.DropIndex(
                name: "IX_ShiftApplicationUsers_ShiftId",
                table: "ShiftApplicationUsers");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "ShiftApplicationUsers");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId1",
                table: "ShiftApplicationUsers");

            migrationBuilder.DropColumn(
                name: "ShiftId",
                table: "ShiftApplicationUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplicationUserId",
                table: "ShiftApplicationUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId1",
                table: "ShiftApplicationUsers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ShiftId",
                table: "ShiftApplicationUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ShiftApplicationUsers_ApplicationUserId1",
                table: "ShiftApplicationUsers",
                column: "ApplicationUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftApplicationUsers_ShiftId",
                table: "ShiftApplicationUsers",
                column: "ShiftId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShiftApplicationUsers_AspNetUsers_ApplicationUserId1",
                table: "ShiftApplicationUsers",
                column: "ApplicationUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShiftApplicationUsers_Shifts_ShiftId",
                table: "ShiftApplicationUsers",
                column: "ShiftId",
                principalTable: "Shifts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
