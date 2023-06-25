using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpraBitirme.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class sdgsdgsdgs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "CouponPrice",
                table: "Order",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CouponPrice",
                table: "Order");
        }
    }
}
