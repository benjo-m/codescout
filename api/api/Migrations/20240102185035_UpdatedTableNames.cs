using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedTableNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Company_Address_AddressId",
                table: "Company");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Company_CompanyId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Position_PositionId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_User_RecruiterId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Technology_Projects_ProjectId",
                table: "Technology");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Address_AddressId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Company_CompanyId",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Technology",
                table: "Technology");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Position",
                table: "Position");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Company",
                table: "Company");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Address",
                table: "Address");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Technology",
                newName: "Technologies");

            migrationBuilder.RenameTable(
                name: "Position",
                newName: "Positions");

            migrationBuilder.RenameTable(
                name: "Company",
                newName: "Companies");

            migrationBuilder.RenameTable(
                name: "Address",
                newName: "Addresses");

            migrationBuilder.RenameIndex(
                name: "IX_User_CompanyId",
                table: "Users",
                newName: "IX_Users_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_User_AddressId",
                table: "Users",
                newName: "IX_Users_AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Technology_ProjectId",
                table: "Technologies",
                newName: "IX_Technologies_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Company_AddressId",
                table: "Companies",
                newName: "IX_Companies_AddressId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Technologies",
                table: "Technologies",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Positions",
                table: "Positions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Companies",
                table: "Companies",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Addresses_AddressId",
                table: "Companies",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Companies_CompanyId",
                table: "Projects",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Positions_PositionId",
                table: "Projects",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Users_RecruiterId",
                table: "Projects",
                column: "RecruiterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Technologies_Projects_ProjectId",
                table: "Technologies",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Addresses_AddressId",
                table: "Users",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Companies_CompanyId",
                table: "Users",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Addresses_AddressId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Companies_CompanyId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Positions_PositionId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Users_RecruiterId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Technologies_Projects_ProjectId",
                table: "Technologies");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Addresses_AddressId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Companies_CompanyId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Technologies",
                table: "Technologies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Positions",
                table: "Positions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Companies",
                table: "Companies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Technologies",
                newName: "Technology");

            migrationBuilder.RenameTable(
                name: "Positions",
                newName: "Position");

            migrationBuilder.RenameTable(
                name: "Companies",
                newName: "Company");

            migrationBuilder.RenameTable(
                name: "Addresses",
                newName: "Address");

            migrationBuilder.RenameIndex(
                name: "IX_Users_CompanyId",
                table: "User",
                newName: "IX_User_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_AddressId",
                table: "User",
                newName: "IX_User_AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Technologies_ProjectId",
                table: "Technology",
                newName: "IX_Technology_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Companies_AddressId",
                table: "Company",
                newName: "IX_Company_AddressId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Technology",
                table: "Technology",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Position",
                table: "Position",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Company",
                table: "Company",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Address",
                table: "Address",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Company_Address_AddressId",
                table: "Company",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Company_CompanyId",
                table: "Projects",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Position_PositionId",
                table: "Projects",
                column: "PositionId",
                principalTable: "Position",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_User_RecruiterId",
                table: "Projects",
                column: "RecruiterId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Technology_Projects_ProjectId",
                table: "Technology",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Address_AddressId",
                table: "User",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Company_CompanyId",
                table: "User",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id");
        }
    }
}
