using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Inicial3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Estados_Id_Estado",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Id_Estado",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Fecha_Registro",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Id_Estado",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "AspNetUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha_Registro",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Id_Estado",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Id_Estado",
                table: "AspNetUsers",
                column: "Id_Estado");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Estados_Id_Estado",
                table: "AspNetUsers",
                column: "Id_Estado",
                principalTable: "Estados",
                principalColumn: "Id_Estado",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
