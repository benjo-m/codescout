using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedAuthRecordTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthRecords_Users_UserId",
                table: "AuthRecords");

            migrationBuilder.DropIndex(
                name: "IX_AuthRecords_UserId",
                table: "AuthRecords");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AuthRecords_UserId",
                table: "AuthRecords",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthRecords_Users_UserId",
                table: "AuthRecords",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
