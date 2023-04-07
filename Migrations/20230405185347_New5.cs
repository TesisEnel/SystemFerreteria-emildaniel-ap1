using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SystemFerreteria.Migrations
{
    /// <inheritdoc />
    public partial class New5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Precio",
                table: "VentaDetalle",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Precio",
                table: "VentaDetalle");
        }
    }
}
