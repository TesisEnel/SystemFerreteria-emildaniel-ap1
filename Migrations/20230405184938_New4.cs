using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SystemFerreteria.Migrations
{
    /// <inheritdoc />
    public partial class New4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Costo",
                table: "VentaDetalle",
                newName: "Importe");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Importe",
                table: "VentaDetalle",
                newName: "Costo");
        }
    }
}
