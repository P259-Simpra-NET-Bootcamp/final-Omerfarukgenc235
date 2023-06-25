using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpraBitirme.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class Mig_asfasfasfasfa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Basket");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "ProductName",
                table: "OrderItem",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ProductPrice",
                table: "OrderItem",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ProductQuantity",
                table: "OrderItem",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ProductTotalPrice",
                table: "OrderItem",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "BasketItem",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "ProductPrice",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "ProductQuantity",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "ProductTotalPrice",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "BasketItem");

            migrationBuilder.AddColumn<double>(
                name: "TotalPrice",
                table: "Basket",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
