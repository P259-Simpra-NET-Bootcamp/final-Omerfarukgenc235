using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpraBitirme.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class sdgsdgsdgsgss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "PointPrice",
                table: "Order",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PointPrice",
                table: "Order");
        }
    }
}
