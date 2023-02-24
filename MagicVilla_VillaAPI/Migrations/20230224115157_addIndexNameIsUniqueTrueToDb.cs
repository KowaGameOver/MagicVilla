using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_VillaAPI.Migrations
{
    public partial class addIndexNameIsUniqueTrueToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "Index_Name",
                table: "Villas");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 2, 24, 13, 51, 57, 592, DateTimeKind.Local).AddTicks(8278));

            migrationBuilder.CreateIndex(
                name: "Index_Name",
                table: "Villas",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "Index_Name",
                table: "Villas");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 2, 24, 13, 30, 2, 67, DateTimeKind.Local).AddTicks(6516));

            migrationBuilder.CreateIndex(
                name: "Index_Name",
                table: "Villas",
                column: "Name");
        }
    }
}
