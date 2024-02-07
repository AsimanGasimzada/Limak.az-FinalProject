using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Limak.Persistence.DAL.Migrations
{
    public partial class initCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AZNBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TRYBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    USDBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "Id", "AZNBalance", "TRYBalance", "USDBalance" },
                values: new object[] { 1, 0m, 0m, 0m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Company");
        }
    }
}
