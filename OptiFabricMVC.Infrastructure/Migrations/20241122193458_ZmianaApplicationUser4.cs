using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OptiFabricMVC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ZmianaApplicationUser4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShiftName",
                table: "Shifts",
                newName: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Shifts",
                newName: "ShiftName");
        }
    }
}
