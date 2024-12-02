using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBaseModel.Migrations
{
    /// <inheritdoc />
    public partial class intialunit66687878 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "Units",
                newName: "StartUnitPrice");

            migrationBuilder.AddColumn<decimal>(
                name: "CurrentUnitPrice",
                table: "Units",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentUnitPrice",
                table: "Units");

            migrationBuilder.RenameColumn(
                name: "StartUnitPrice",
                table: "Units",
                newName: "UnitPrice");
        }
    }
}
