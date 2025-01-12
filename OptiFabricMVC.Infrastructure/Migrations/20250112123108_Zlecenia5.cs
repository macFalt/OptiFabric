using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OptiFabricMVC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Zlecenia5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ActivEmployeeJob",
                table: "Jobs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivEmployeeJob",
                table: "Jobs");
        }
    }
}
