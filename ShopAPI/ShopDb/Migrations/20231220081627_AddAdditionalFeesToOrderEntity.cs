using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopDb.Migrations
{
    /// <inheritdoc />
    public partial class AddAdditionalFeesToOrderEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "AdditionalFees",
                table: "Orders",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionalFees",
                table: "Orders");
        }
    }
}
