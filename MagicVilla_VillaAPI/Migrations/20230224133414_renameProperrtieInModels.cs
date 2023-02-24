using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_VillaAPI.Migrations
{
    public partial class renameProperrtieInModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VillaNumbers_Villas_VillaID",
                table: "VillaNumbers");

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Villas",
                newName: "VillaId");

            migrationBuilder.RenameColumn(
                name: "VillaID",
                table: "VillaNumbers",
                newName: "VillaId");

            migrationBuilder.RenameColumn(
                name: "VillaNo",
                table: "VillaNumbers",
                newName: "VillaNumberId");

            migrationBuilder.RenameIndex(
                name: "IX_VillaNumbers_VillaID",
                table: "VillaNumbers",
                newName: "IX_VillaNumbers_VillaId");

            migrationBuilder.AddForeignKey(
                name: "FK_VillaNumbers_Villas_VillaId",
                table: "VillaNumbers",
                column: "VillaId",
                principalTable: "Villas",
                principalColumn: "VillaId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VillaNumbers_Villas_VillaId",
                table: "VillaNumbers");

            migrationBuilder.RenameColumn(
                name: "VillaId",
                table: "Villas",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "VillaId",
                table: "VillaNumbers",
                newName: "VillaID");

            migrationBuilder.RenameColumn(
                name: "VillaNumberId",
                table: "VillaNumbers",
                newName: "VillaNo");

            migrationBuilder.RenameIndex(
                name: "IX_VillaNumbers_VillaId",
                table: "VillaNumbers",
                newName: "IX_VillaNumbers_VillaID");

            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenity", "CreatedDate", "Details", "ImageUrl", "Name", "Occupancy", "Rate", "Sqft", "UpdatedDate" },
                values: new object[] { 1, "", new DateTime(2023, 2, 24, 13, 51, 57, 592, DateTimeKind.Local).AddTicks(8278), "Ocean View by Exceptional Villas is a lovely 3 bedroom villa set on 2 levels and located on the prestigious Royal Westmoreland Golf Club. Guests staying at here have panoramic view of the Caribbean Sea while overlooking a beautiful pond and fountain which is home to local birds. This is a very well designed villa and has a homely feel to it. It can be comfortably rented as a one, two or three bedroom and has a communal swimming pool and golf access payable locally.", "", "Royal Villa", 5, 200.0, 550, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.AddForeignKey(
                name: "FK_VillaNumbers_Villas_VillaID",
                table: "VillaNumbers",
                column: "VillaID",
                principalTable: "Villas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
