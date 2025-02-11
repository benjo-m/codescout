using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class SocialMediaLinkToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SocialMediaLinks_UserId",
                table: "SocialMediaLinks",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SocialMediaLinks_Users_UserId",
                table: "SocialMediaLinks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SocialMediaLinks_Users_UserId",
                table: "SocialMediaLinks");

            migrationBuilder.DropIndex(
                name: "IX_SocialMediaLinks_UserId",
                table: "SocialMediaLinks");
        }
    }
}
