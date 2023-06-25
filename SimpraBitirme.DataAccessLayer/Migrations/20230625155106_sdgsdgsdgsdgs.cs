using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpraBitirme.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class sdgsdgsdgsdgs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrderNumber",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderNumber",
                table: "Order");
        }
    }
}
