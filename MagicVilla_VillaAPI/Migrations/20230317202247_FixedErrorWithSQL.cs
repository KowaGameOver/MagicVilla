using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_VillaAPI.Migrations
{
    public partial class FixedErrorWithSQL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rents_VillaNumbers_VillaID",
                table: "Rents");

            migrationBuilder.DropIndex(
                name: "IX_Rents_VillaID",
                table: "Rents");

            migrationBuilder.RenameColumn(
                name: "VillaID",
                table: "Rents",
                newName: "VillaId");

            migrationBuilder.CreateIndex(
                name: "IX_Rents_numberOfVilla",
                table: "Rents",
                column: "numberOfVilla");

            migrationBuilder.AddForeignKey(
                name: "FK_Rents_VillaNumbers_numberOfVilla",
                table: "Rents",
                column: "numberOfVilla",
                principalTable: "VillaNumbers",
                principalColumn: "VillaNo",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rents_VillaNumbers_numberOfVilla",
                table: "Rents");

            migrationBuilder.DropIndex(
                name: "IX_Rents_numberOfVilla",
                table: "Rents");

            migrationBuilder.RenameColumn(
                name: "VillaId",
                table: "Rents",
                newName: "VillaID");

            migrationBuilder.CreateIndex(
                name: "IX_Rents_VillaID",
                table: "Rents",
                column: "VillaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Rents_VillaNumbers_VillaID",
                table: "Rents",
                column: "VillaID",
                principalTable: "VillaNumbers",
                principalColumn: "VillaNo",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
