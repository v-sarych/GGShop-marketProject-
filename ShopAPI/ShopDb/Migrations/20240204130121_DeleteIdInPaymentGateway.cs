using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopDb.Migrations
{
    /// <inheritdoc />
    public partial class DeleteIdInPaymentGateway : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdInPaymentGateway",
                table: "Payments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdInPaymentGateway",
                table: "Payments",
                type: "text",
                nullable: true);
        }
    }
}
