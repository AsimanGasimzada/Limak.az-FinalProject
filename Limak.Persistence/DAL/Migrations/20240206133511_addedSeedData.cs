using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Limak.Persistence.DAL.Migrations
{
    public partial class addedSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Citizenships",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Azərbaycanlı" },
                    { 2, "Digər" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "CreatedBy", "CreatedTime", "IsDeleted", "ModifiedBy", "ModifiedTime", "Name" },
                values: new object[,]
                {
                    { 1, "Admin", new DateTime(2024, 2, 6, 13, 35, 11, 74, DateTimeKind.Utc).AddTicks(9935), false, "Admin", new DateTime(2024, 2, 6, 13, 35, 11, 74, DateTimeKind.Utc).AddTicks(9937), "Türkiyə" },
                    { 2, "Admin", new DateTime(2024, 2, 6, 13, 35, 11, 74, DateTimeKind.Utc).AddTicks(9939), false, "Admin", new DateTime(2024, 2, 6, 13, 35, 11, 74, DateTimeKind.Utc).AddTicks(9940), "Amerika" }
                });

            migrationBuilder.InsertData(
                table: "UserPositions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Fiziki şəxs" },
                    { 2, "Mülki şəxs" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Citizenships",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Citizenships",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserPositions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserPositions",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
