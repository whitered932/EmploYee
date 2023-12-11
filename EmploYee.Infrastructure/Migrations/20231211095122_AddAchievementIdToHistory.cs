using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmploYee.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAchievementIdToHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AchievementId",
                table: "EmployeeAchievementHistory",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AchievementId",
                table: "EmployeeAchievementHistory");
        }
    }
}
