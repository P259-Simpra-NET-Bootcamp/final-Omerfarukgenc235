using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpraBitirme.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class deneme3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureUrl",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "OrderItem");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Product",
                type: "float",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Price",
                table: "Product",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<string>(
                name: "PictureUrl",
                table: "OrderItem",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "OrderItem",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "OrderItem",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
