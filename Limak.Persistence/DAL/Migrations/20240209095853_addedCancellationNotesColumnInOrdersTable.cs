using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Limak.Persistence.DAL.Migrations
{
    public partial class addedCancellationNotesColumnInOrdersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CancellationNotes",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CancellationNotes",
                table: "Orders");
        }
    }
}
