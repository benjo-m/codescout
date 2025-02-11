using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class FixedProjectUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectUser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectUser",
                columns: table => new
                {
                    CandidatesId = table.Column<int>(type: "int", nullable: false),
                    ProjectsAppliedToId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectUser", x => new { x.CandidatesId, x.ProjectsAppliedToId });
                    table.ForeignKey(
                        name: "FK_ProjectUser_Projects_ProjectsAppliedToId",
                        column: x => x.ProjectsAppliedToId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectUser_Users_CandidatesId",
                        column: x => x.CandidatesId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectUser_ProjectsAppliedToId",
                table: "ProjectUser",
                column: "ProjectsAppliedToId");
        }
    }
}
