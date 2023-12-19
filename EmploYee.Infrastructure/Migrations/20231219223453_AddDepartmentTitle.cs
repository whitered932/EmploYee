using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmploYee.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDepartmentTitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DepartmentTitle",
                table: "Users",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepartmentTitle",
                table: "Users");
        }
    }
}
