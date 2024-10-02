using Microsoft.EntityFrameworkCore.Migrations;

namespace Medcard.DbAccessLayer.Migrations
{
    public partial class RecDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Descriptions",
                table: "Recomendations",
                newName: "Description");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Recomendations",
                newName: "Descriptions");
        }
    }
}
