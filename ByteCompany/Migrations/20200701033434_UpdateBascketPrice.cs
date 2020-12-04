using Microsoft.EntityFrameworkCore.Migrations;

namespace ByteCompany.Migrations
{
    public partial class UpdateBascketPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewPrice",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "OldPrice",
                table: "Baskets");

            migrationBuilder.AddColumn<decimal>(
                name: "BascketPrice",
                table: "Baskets",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BascketPrice",
                table: "Baskets");

            migrationBuilder.AddColumn<decimal>(
                name: "NewPrice",
                table: "Baskets",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "OldPrice",
                table: "Baskets",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
