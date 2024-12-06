using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBaseModel.Migrations
{
    /// <inheritdoc />
    public partial class relationLocationRoi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocationRoiId",
                table: "Units",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "unit1",
                column: "LocationRoiId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "unit2",
                column: "LocationRoiId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Units_LocationRoiId",
                table: "Units",
                column: "LocationRoiId");

            migrationBuilder.AddForeignKey(
                name: "FK_Units_LocationRois_LocationRoiId",
                table: "Units",
                column: "LocationRoiId",
                principalTable: "LocationRois",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Units_LocationRois_LocationRoiId",
                table: "Units");

            migrationBuilder.DropIndex(
                name: "IX_Units_LocationRoiId",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "LocationRoiId",
                table: "Units");
        }
    }
}
