using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class AddedWorkArrangementTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WorkArrangementId",
                table: "Projects",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "WorkArrangements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkArrangements", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_WorkArrangementId",
                table: "Projects",
                column: "WorkArrangementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_WorkArrangements_WorkArrangementId",
                table: "Projects",
                column: "WorkArrangementId",
                principalTable: "WorkArrangements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_WorkArrangements_WorkArrangementId",
                table: "Projects");

            migrationBuilder.DropTable(
                name: "WorkArrangements");

            migrationBuilder.DropIndex(
                name: "IX_Projects_WorkArrangementId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "WorkArrangementId",
                table: "Projects");
        }
    }
}
