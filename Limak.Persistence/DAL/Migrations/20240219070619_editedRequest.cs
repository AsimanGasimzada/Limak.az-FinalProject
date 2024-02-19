using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Limak.Persistence.DAL.Migrations
{
    public partial class editedRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "Requests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OperatorId",
                table: "Requests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_AppUserId",
                table: "Requests",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_OperatorId",
                table: "Requests",
                column: "OperatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_AspNetUsers_AppUserId",
                table: "Requests",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_AspNetUsers_OperatorId",
                table: "Requests",
                column: "OperatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_AspNetUsers_AppUserId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_AspNetUsers_OperatorId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_AppUserId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_OperatorId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "OperatorId",
                table: "Requests");
        }
    }
}
