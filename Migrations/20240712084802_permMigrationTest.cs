using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MwTesting.Migrations
{
    /// <inheritdoc />
    public partial class permMigrationTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserPerms",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    permission = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPerms", x => new { x.UserId, x.permission });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPerms");
        }
    }
}
