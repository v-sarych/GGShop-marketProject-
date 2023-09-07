using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopDb.Migrations
{
    public partial class DeleteSessionJwtIdKeyAndSetUniq : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Sessions_JwtId",
                table: "Sessions");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_JwtId",
                table: "Sessions",
                column: "JwtId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Sessions_JwtId",
                table: "Sessions");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Sessions_JwtId",
                table: "Sessions",
                column: "JwtId");
        }
    }
}
