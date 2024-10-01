using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Medcard.DbAccessLayer.Migrations
{
    public partial class AddRecomendations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recomendations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Descriptions = table.Column<string>(type: "text", nullable: true),
                    PetId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recomendations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recomendations_Pets_PetId",
                        column: x => x.PetId,
                        principalTable: "Pets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recomendations_PetId",
                table: "Recomendations",
                column: "PetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recomendations");
        }
    }
}
