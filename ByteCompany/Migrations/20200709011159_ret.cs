using Microsoft.EntityFrameworkCore.Migrations;

namespace ByteCompany.Migrations
{
    public partial class ret : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ModelsId",
                table: "Products",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ModelsId",
                table: "Products",
                column: "ModelsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_ModelsId",
                table: "Products",
                column: "ModelsId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_ModelsId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ModelsId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ModelsId",
                table: "Products");
        }
    }
}
