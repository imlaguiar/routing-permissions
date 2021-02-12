using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace routing.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Funcionalidades",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionalidades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rotas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(nullable: true),
                    Componente = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rotas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RotasFuncionalidades",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdFuncionalidade = table.Column<int>(nullable: false),
                    IdRota = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RotasFuncionalidades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RotasFuncionalidades_Funcionalidades_IdFuncionalidade",
                        column: x => x.IdFuncionalidade,
                        principalTable: "Funcionalidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RotasFuncionalidades_Rotas_IdRota",
                        column: x => x.IdRota,
                        principalTable: "Rotas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuariosRotasFuncionalidades",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdUsuario = table.Column<int>(nullable: false),
                    IdRotaFuncionalidade = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosRotasFuncionalidades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuariosRotasFuncionalidades_RotasFuncionalidades_IdRotaFunc~",
                        column: x => x.IdRotaFuncionalidade,
                        principalTable: "RotasFuncionalidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuariosRotasFuncionalidades_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RotasFuncionalidades_IdFuncionalidade",
                table: "RotasFuncionalidades",
                column: "IdFuncionalidade");

            migrationBuilder.CreateIndex(
                name: "IX_RotasFuncionalidades_IdRota",
                table: "RotasFuncionalidades",
                column: "IdRota");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosRotasFuncionalidades_IdRotaFuncionalidade",
                table: "UsuariosRotasFuncionalidades",
                column: "IdRotaFuncionalidade");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosRotasFuncionalidades_IdUsuario",
                table: "UsuariosRotasFuncionalidades",
                column: "IdUsuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuariosRotasFuncionalidades");

            migrationBuilder.DropTable(
                name: "RotasFuncionalidades");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Funcionalidades");

            migrationBuilder.DropTable(
                name: "Rotas");
        }
    }
}
