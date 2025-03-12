using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SupermarketAPI.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CATEGORIAS",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NOME = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    DATACADASTRO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CATEGORIAS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PRODUTOS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NOME = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    PRECO = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    QUANTIDADE = table.Column<int>(type: "int", nullable: false),
                    DATACADASTRO = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CATEGORIA_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUTOS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PRODUTOS_CATEGORIAS_CATEGORIA_ID",
                        column: x => x.CATEGORIA_ID,
                        principalTable: "CATEGORIAS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CATEGORIAS_NOME",
                table: "CATEGORIAS",
                column: "NOME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PRODUTOS_CATEGORIA_ID",
                table: "PRODUTOS",
                column: "CATEGORIA_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PRODUTOS");

            migrationBuilder.DropTable(
                name: "CATEGORIAS");
        }
    }
}
