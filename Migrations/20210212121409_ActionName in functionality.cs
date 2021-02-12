using Microsoft.EntityFrameworkCore.Migrations;

namespace routing.Migrations
{
    public partial class ActionNameinfunctionality : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ActionName",
                table: "Funcionalidades",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActionName",
                table: "Funcionalidades");
        }
    }
}
