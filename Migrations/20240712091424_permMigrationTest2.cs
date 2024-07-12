using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MwTesting.Migrations
{
    /// <inheritdoc />
    public partial class permMigrationTest2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_UserPerms_Users_UserId",
                table: "UserPerms",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPerms_Users_UserId",
                table: "UserPerms");
        }
    }
}
