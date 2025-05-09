using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test_API.Migrations
{
    /// <inheritdoc />
    public partial class EnableCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persona",
                columns: table => new
                {
                    idPersona = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    apellidoPaterno = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    apellidoMaterno = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    identificacion = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Persona__A4788141C5E3AFDC", x => x.idPersona);
                });

            migrationBuilder.CreateTable(
                name: "Factura",
                columns: table => new
                {
                    idFactura = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fecha = table.Column<DateOnly>(type: "date", nullable: true),
                    monto = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    personaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Factura__3CD5687E3A494924", x => x.idFactura);
                    table.ForeignKey(
                        name: "FK__Factura__persona__398D8EEE",
                        column: x => x.personaId,
                        principalTable: "Persona",
                        principalColumn: "idPersona",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Factura_personaId",
                table: "Factura",
                column: "personaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Factura");

            migrationBuilder.DropTable(
                name: "Persona");
        }
    }
}
