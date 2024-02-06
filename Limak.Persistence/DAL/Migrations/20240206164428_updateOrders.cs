using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Limak.Persistence.DAL.Migrations
{
    public partial class updateOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "ModifiedTime",
                table: "Countries");

            migrationBuilder.AlterColumn<string>(
                name: "AdditionFeesNotes",
                table: "Orders",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AddColumn<bool>(
                name: "IsCancel",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCancel",
                table: "Orders");

            migrationBuilder.AlterColumn<string>(
                name: "AdditionFeesNotes",
                table: "Orders",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Countries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "Countries",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Countries",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Countries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedTime",
                table: "Countries",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedBy", "CreatedTime", "ModifiedBy", "ModifiedTime" },
                values: new object[] { "Admin", new DateTime(2024, 2, 6, 13, 35, 11, 74, DateTimeKind.Utc).AddTicks(9935), "Admin", new DateTime(2024, 2, 6, 13, 35, 11, 74, DateTimeKind.Utc).AddTicks(9937) });

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedBy", "CreatedTime", "ModifiedBy", "ModifiedTime" },
                values: new object[] { "Admin", new DateTime(2024, 2, 6, 13, 35, 11, 74, DateTimeKind.Utc).AddTicks(9939), "Admin", new DateTime(2024, 2, 6, 13, 35, 11, 74, DateTimeKind.Utc).AddTicks(9940) });
        }
    }
}
