using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Limak.Persistence.DAL.Migrations
{
    public partial class changeKargomat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Position",
                table: "Kargomats",
                newName: "CordinateY");

            migrationBuilder.AddColumn<string>(
                name: "CordinateX",
                table: "Kargomats",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CordinateX",
                table: "Kargomats");

            migrationBuilder.RenameColumn(
                name: "CordinateY",
                table: "Kargomats",
                newName: "Position");
        }
    }
}
