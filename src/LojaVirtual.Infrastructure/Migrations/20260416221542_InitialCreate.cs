using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojaVirtual.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TB_CATEGORIA",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    NOME_CATEGORIA = table.Column<string>(type: "VARCHAR(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DESC_CATEGORIA = table.Column<string>(type: "VARCHAR(1000)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CRIADO_EM = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    ATIVO = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CATEGORIA_ID", x => x.ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TB_CLIENTE",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    NOME_CLIENTE = table.Column<string>(type: "VARCHAR(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TELEFONE_CLIENTE = table.Column<string>(type: "VARCHAR(20)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CPF_CLIENTE = table.Column<string>(type: "VARCHAR(15)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CRIADO_EM = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    ATIVO = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLIENTE_ID", x => x.ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TB_LOJA",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    NOME_LOJA = table.Column<string>(type: "VARCHAR(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CNPJ_LOJA = table.Column<string>(type: "VARCHAR(18)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CRIADO_EM = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    ATIVO = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOJA_ID", x => x.ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TB_PRODUTO",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    NOME_PROD = table.Column<string>(type: "VARCHAR(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PRECO_PROD = table.Column<decimal>(type: "DECIMAL(10,2)", nullable: false),
                    CLIENTE_ID = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    CRIADO_EM = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    ATIVO = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUTO_ID", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PRODUTO_CLIENTE",
                        column: x => x.CLIENTE_ID,
                        principalTable: "TB_CLIENTE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TB_ESTOQUE",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    QNTD_ESTOQ = table.Column<int>(type: "INT", nullable: false),
                    VALIDADE_ESTOQ = table.Column<DateTime>(type: "DATE", nullable: true),
                    LOJA_ID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CRIADO_EM = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    ATIVO = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ESTOQUE_ID", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ESTOQUE_LOJA",
                        column: x => x.LOJA_ID,
                        principalTable: "TB_LOJA",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TB_CATEGORIA_PRODUTO",
                columns: table => new
                {
                    CATEGORIA_ID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PRODUTO_ID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CRIADO_EM = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    ATIVO = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CATEGORIA_PRODUTO", x => new { x.CATEGORIA_ID, x.PRODUTO_ID });
                    table.ForeignKey(
                        name: "FK_CATEGORIA_PRODUTO_CATEGORIA",
                        column: x => x.CATEGORIA_ID,
                        principalTable: "TB_CATEGORIA",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CATEGORIA_PRODUTO_PRODUTO",
                        column: x => x.PRODUTO_ID,
                        principalTable: "TB_PRODUTO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TB_ESTOQUE_PRODUTO",
                columns: table => new
                {
                    ESTOQUE_ID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PRODUTO_ID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CRIADO_EM = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    ATIVO = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ESTOQUE_PRODUTO", x => new { x.ESTOQUE_ID, x.PRODUTO_ID });
                    table.ForeignKey(
                        name: "FK_ESTOQUE_PRODUTO_ESTOQUE",
                        column: x => x.ESTOQUE_ID,
                        principalTable: "TB_ESTOQUE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ESTOQUE_PRODUTO_PRODUTO",
                        column: x => x.PRODUTO_ID,
                        principalTable: "TB_PRODUTO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CATEGORIA_PRODUTO_CATEGORIA",
                table: "TB_CATEGORIA_PRODUTO",
                column: "CATEGORIA_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CATEGORIA_PRODUTO_PRODUTO",
                table: "TB_CATEGORIA_PRODUTO",
                column: "PRODUTO_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CLIENTE_CPF",
                table: "TB_CLIENTE",
                column: "CPF_CLIENTE",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ESTOQUE_LOJA",
                table: "TB_ESTOQUE",
                column: "LOJA_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ESTOQUE_PRODUTO_ESTOQUE",
                table: "TB_ESTOQUE_PRODUTO",
                column: "ESTOQUE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ESTOQUE_PRODUTO_PRODUTO",
                table: "TB_ESTOQUE_PRODUTO",
                column: "PRODUTO_ID");

            migrationBuilder.CreateIndex(
                name: "IX_LOJA_CNPJ",
                table: "TB_LOJA",
                column: "CNPJ_LOJA",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PRODUTO_CLIENTE",
                table: "TB_PRODUTO",
                column: "CLIENTE_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_CATEGORIA_PRODUTO");

            migrationBuilder.DropTable(
                name: "TB_ESTOQUE_PRODUTO");

            migrationBuilder.DropTable(
                name: "TB_CATEGORIA");

            migrationBuilder.DropTable(
                name: "TB_ESTOQUE");

            migrationBuilder.DropTable(
                name: "TB_PRODUTO");

            migrationBuilder.DropTable(
                name: "TB_LOJA");

            migrationBuilder.DropTable(
                name: "TB_CLIENTE");
        }
    }
}
