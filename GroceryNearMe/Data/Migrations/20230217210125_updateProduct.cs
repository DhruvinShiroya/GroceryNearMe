using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroceryNearMe.Data.Migrations
{
    public partial class updateProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Reviews_ReviewId1",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ReviewId1",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ReviewId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ReviewId1",
                table: "Products");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ProductID",
                table: "Reviews",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Products_ProductID",
                table: "Reviews",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Products_ProductID",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_ProductID",
                table: "Reviews");

            migrationBuilder.AddColumn<int>(
                name: "ReviewId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReviewId1",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ReviewId1",
                table: "Products",
                column: "ReviewId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Reviews_ReviewId1",
                table: "Products",
                column: "ReviewId1",
                principalTable: "Reviews",
                principalColumn: "ReviewId");
        }
    }
}
