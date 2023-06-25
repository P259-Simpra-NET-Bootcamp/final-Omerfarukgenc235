using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpraBitirme.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class sdgds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountCode",
                table: "Basket");

            migrationBuilder.RenameColumn(
                name: "DiscountPrice",
                table: "Basket",
                newName: "CouponPrice");

            migrationBuilder.AddColumn<string>(
                name: "CouponCode",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TotalPrice",
                table: "Order",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Coupon",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<double>(
                name: "TotalPrice",
                table: "Basket",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "CouponCode",
                table: "Basket",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CouponCode",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "CouponCode",
                table: "Basket");

            migrationBuilder.RenameColumn(
                name: "CouponPrice",
                table: "Basket",
                newName: "DiscountPrice");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Coupon",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPrice",
                table: "Basket",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<string>(
                name: "DiscountCode",
                table: "Basket",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
