using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataBaseModel.Migrations
{
    /// <inheritdoc />
    public partial class seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Developers",
                columns: new[] { "Id", "Name", "PhoneNumber", "RealEstateRegistry", "SizeOfCompany" },
                values: new object[,]
                {
                    { "dev1", "Developer A", null, null, null },
                    { "dev2", "Developer B", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Available", "AvailableShares", "AvilableDate", "CurrentUnitPrice", "CurrentUnitROI", "DeveloperId", "DownPayment", "ExitDate", "Location", "MonthlyPayment", "Name", "StartUnitPrice" },
                values: new object[,]
                {
                    { "unit1", 0, 10, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1200000.00m, 15.00m, "dev1", 50000.00m, new DateTime(2027, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "New Cairo", 15000.00m, "Luxury Apartment", 1000000.00m },
                    { "unit2", 1, 5, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5500000.00m, 10.00m, "dev2", 100000.00m, new DateTime(2030, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sheikh Zayed", 50000.00m, "Family Villa", 5000000.00m }
                });

            migrationBuilder.InsertData(
                table: "UnitDescriptions",
                columns: new[] { "Id", "Area", "NumberOfBathrooms", "NumberOfBedrooms", "UnitId" },
                values: new object[,]
                {
                    { 1, 120, 2, 3, "unit1" },
                    { 2, 350, 4, 5, "unit2" }
                });

            migrationBuilder.InsertData(
                table: "UnitView",
                columns: new[] { "Id", "Name", "UnitId" },
                values: new object[,]
                {
                    { 1, 7, "unit1" },
                    { 2, 3, "unit1" },
                    { 3, 4, "unit2" },
                    { 4, 0, "unit2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UnitDescriptions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UnitDescriptions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UnitView",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UnitView",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UnitView",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "UnitView",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "unit1");

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: "unit2");

            migrationBuilder.DeleteData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: "dev1");

            migrationBuilder.DeleteData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: "dev2");
        }
    }
}
