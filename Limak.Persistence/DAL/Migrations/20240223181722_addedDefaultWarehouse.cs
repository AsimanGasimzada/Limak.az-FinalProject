using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Limak.Persistence.DAL.Migrations
{
    public partial class addedDefaultWarehouse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Warehouses",
                columns: new[] { "Id", "CreatedBy", "CreatedTime", "Email", "IsDeleted", "Location", "ModifiedBy", "ModifiedTime", "Name", "PhoneNumber", "Position", "WorkingHours" },
                values: new object[] { 1, "Admin", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Default@gmail.com", true, "Default", "Admin", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Default", "Default", "Default", "Default warehouse" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Warehouses",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
