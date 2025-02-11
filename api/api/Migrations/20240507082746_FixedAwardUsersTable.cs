using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class FixedAwardUsersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AwardUser");

            migrationBuilder.CreateIndex(
                name: "IX_AwardUsers_UserId",
                table: "AwardUsers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AwardUsers_Awards_AwardId",
                table: "AwardUsers",
                column: "AwardId",
                principalTable: "Awards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AwardUsers_Users_UserId",
                table: "AwardUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AwardUsers_Awards_AwardId",
                table: "AwardUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AwardUsers_Users_UserId",
                table: "AwardUsers");

            migrationBuilder.DropIndex(
                name: "IX_AwardUsers_UserId",
                table: "AwardUsers");

            migrationBuilder.CreateTable(
                name: "AwardUser",
                columns: table => new
                {
                    AwardsId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AwardUser", x => new { x.AwardsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_AwardUser_Awards_AwardsId",
                        column: x => x.AwardsId,
                        principalTable: "Awards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AwardUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AwardUser_UsersId",
                table: "AwardUser",
                column: "UsersId");
        }
    }
}
