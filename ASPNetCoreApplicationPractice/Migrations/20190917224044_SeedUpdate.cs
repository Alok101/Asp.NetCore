using Microsoft.EntityFrameworkCore.Migrations;

namespace ASPNetCoreApplicationPractice.Migrations
{
    public partial class SeedUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Address", "Department", "Name", "Salary" },
                values: new object[,]
                {
                    { 1, "India1", 6, "Alok1 Gautam", 1200000L },
                    { 2, "India2", 5, "Alok2 Gautam", 2200000L },
                    { 3, "India3", 2, "Alok3 Gautam", 3200000L },
                    { 4, "India4", 1, "Alok4 Gautam", 4200000L },
                    { 5, "India5", 3, "Alok5 Gautam", 5200000L },
                    { 6, "India6", 4, "Alok6 Gautam", 6200000L },
                    { 7, "India7", 7, "Alok7 Gautam", 7200000L },
                    { 8, "India8", 0, "Alok8 Gautam", 7200000L }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 8);
        }
    }
}
