using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Limak.Persistence.DAL.Migrations
{
    public partial class changeDeliveries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "Deliveries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_AppUserId",
                table: "Deliveries",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_AspNetUsers_AppUserId",
                table: "Deliveries",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_AspNetUsers_AppUserId",
                table: "Deliveries");

            migrationBuilder.DropIndex(
                name: "IX_Deliveries_AppUserId",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Deliveries");
        }
    }
}
