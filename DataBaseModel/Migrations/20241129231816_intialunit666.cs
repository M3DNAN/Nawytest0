using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBaseModel.Migrations
{
    /// <inheritdoc />
    public partial class intialunit666 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UnitROI",
                table: "Units",
                newName: "CurrentUnitROI");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CurrentUnitROI",
                table: "Units",
                newName: "UnitROI");
        }
    }
}
