using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBaseModel.Migrations
{
    /// <inheritdoc />
    public partial class intialadnan2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnitImages_Units_UnitId1",
                table: "UnitImages");

            migrationBuilder.DropIndex(
                name: "IX_UnitImages_UnitId1",
                table: "UnitImages");

            migrationBuilder.DropColumn(
                name: "UnitId1",
                table: "UnitImages");

            migrationBuilder.AlterColumn<string>(
                name: "UnitId",
                table: "UnitImages",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_UnitImages_UnitId",
                table: "UnitImages",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_UnitImages_Units_UnitId",
                table: "UnitImages",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnitImages_Units_UnitId",
                table: "UnitImages");

            migrationBuilder.DropIndex(
                name: "IX_UnitImages_UnitId",
                table: "UnitImages");

            migrationBuilder.AlterColumn<int>(
                name: "UnitId",
                table: "UnitImages",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UnitId1",
                table: "UnitImages",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UnitImages_UnitId1",
                table: "UnitImages",
                column: "UnitId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UnitImages_Units_UnitId1",
                table: "UnitImages",
                column: "UnitId1",
                principalTable: "Units",
                principalColumn: "Id");
        }
    }
}
