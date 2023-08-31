using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_VillaAPI.Migrations
{
    public partial class AddVillaNumberToRentsToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rents_VillaNumbers_VillaNum",
                table: "Rents");

            migrationBuilder.DropIndex(
                name: "IX_Rents_VillaNum",
                table: "Rents");

            migrationBuilder.RenameColumn(
                name: "VillaNum",
                table: "Rents",
                newName: "VillaNumber");

            migrationBuilder.AddColumn<int>(
                name: "VillaID",
                table: "Rents",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rents_VillaNumbers_VillaID",
                table: "Rents");

            migrationBuilder.DropIndex(
                name: "IX_Rents_VillaID",
                table: "Rents");

            migrationBuilder.DropColumn(
                name: "VillaID",
                table: "Rents");

            migrationBuilder.RenameColumn(
                name: "VillaNumber",
                table: "Rents",
                newName: "VillaNum");

            migrationBuilder.CreateIndex(
                name: "IX_Rents_VillaNum",
                table: "Rents",
                column: "VillaNum");

            migrationBuilder.AddForeignKey(
                name: "FK_Rents_VillaNumbers_VillaNum",
                table: "Rents",
                column: "VillaNum",
                principalTable: "VillaNumbers",
                principalColumn: "VillaNo",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
