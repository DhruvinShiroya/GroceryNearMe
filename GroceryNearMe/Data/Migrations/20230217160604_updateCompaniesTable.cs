using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroceryNearMe.Data.Migrations
{
    public partial class updateCompaniesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Companies");
        }
    }
}
