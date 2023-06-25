using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpraBitirme.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class mig_csdgsdgsd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "PointBalance",
                table: "User",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PointBalance",
                table: "User",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
