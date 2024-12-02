using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBaseModel.Migrations
{
    /// <inheritdoc />
    public partial class intialunit444 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FixedShares",
                table: "Units");

            migrationBuilder.AddColumn<int>(
                name: "AvalibaleSharesNumber",
                table: "SharesTransactions",
                type: "int",
                nullable: true,
                defaultValue: 3);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvalibaleSharesNumber",
                table: "SharesTransactions");

            migrationBuilder.AddColumn<int>(
                name: "FixedShares",
                table: "Units",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
