using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopDb.Migrations
{
    public partial class AddSessionRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductTag_Products_ProductId",
                table: "ProductTag");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTag_Tags_TagId",
                table: "ProductTag");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "User");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Sessions",
                type: "text",
                nullable: false,
                defaultValue: "User");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Sessions_JwtId",
                table: "Sessions",
                column: "JwtId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTag_Products_ProductId",
                table: "ProductTag",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTag_Tags_TagId",
                table: "ProductTag",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductTag_Products_ProductId",
                table: "ProductTag");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTag_Tags_TagId",
                table: "ProductTag");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Sessions_JwtId",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Sessions");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTag_Products_ProductId",
                table: "ProductTag",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTag_Tags_TagId",
                table: "ProductTag",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
