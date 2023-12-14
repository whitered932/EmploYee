using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EmploYee.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ToJson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeAchievementHistory");

            migrationBuilder.AddColumn<string>(
                name: "AchievementHistories",
                table: "Users",
                type: "jsonb",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AchievementHistories",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "EmployeeAchievementHistory",
                columns: table => new
                {
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AchievementId = table.Column<long>(type: "bigint", nullable: true),
                    CurrentValue = table.Column<double>(type: "double precision", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeAchievementHistory", x => new { x.EmployeeId, x.Id });
                    table.ForeignKey(
                        name: "FK_EmployeeAchievementHistory_Users_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
