using Microsoft.EntityFrameworkCore.Migrations;

namespace ByteCompany.Migrations
{
    public partial class AddRoleAuthorize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetRoles_IdentityRoleId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_IdentityRoleId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IdentityRoleId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Masters",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Masters_UserId",
                table: "Masters",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Masters_AspNetUsers_UserId",
                table: "Masters",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Masters_AspNetUsers_UserId",
                table: "Masters");

            migrationBuilder.DropIndex(
                name: "IX_Masters_UserId",
                table: "Masters");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Masters");

            migrationBuilder.AddColumn<string>(
                name: "IdentityRoleId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_IdentityRoleId",
                table: "AspNetUsers",
                column: "IdentityRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetRoles_IdentityRoleId",
                table: "AspNetUsers",
                column: "IdentityRoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
