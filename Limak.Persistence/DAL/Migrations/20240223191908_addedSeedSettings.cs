using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Limak.Persistence.DAL.Migrations
{
    public partial class addedSeedSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "Key", "Value" },
                values: new object[,]
                {
                    { 1, "HeaderLogo", "Image" },
                    { 2, "FooterLogo", "Image" },
                    { 3, "PhoneNumber", "number" },
                    { 4, "XaricdekiUnvanlar-AdSoyad", "0256069 - LİMAK TAŞIMACILIK DIŞ TİCARET LİMİTED ŞİRKETİ" },
                    { 5, "XaricdekiUnvanlar-AdressBasligi", "LIMAK" },
                    { 6, "XaricdekiUnvanlar-AdressSatir", "Gürsel mah.Bahçeler cad. Erdoğan iş merkezi 37-39B" },
                    { 7, "XaricdekiUnvanlar-IlSehir", "Istanbul" },
                    { 8, "XaricdekiUnvanlar-Semt", "Gürsel" },
                    { 9, "XaricdekiUnvanlar-TCKimlik", "35650276048" },
                    { 10, "XaricdekiUnvanlar-İlce", "Kağıthane" },
                    { 11, "XaricdekiUnvanlar-PostKodu", "34400" },
                    { 12, "XaricdekiUnvanlar-Telefon", "05364700021" },
                    { 13, "XaricdekiUnvanlar-VergiDairesi", "Şişli" },
                    { 14, "XaricdekiUnvanlar-Ulke", "Türkiye" },
                    { 15, "XaricdekiUnvanlar-VergiNo", "6081089593" },
                    { 16, "XaricdekiUnvanlar-TrendyolSMS", "05356191259" },
                    { 17, "XaricdekiUnvanlar-AddressLine1", "320 Cornell drive C1" },
                    { 18, "XaricdekiUnvanlar-State", "Delaware (DE) " },
                    { 19, "XaricdekiUnvanlar-City", "Wilmington " },
                    { 20, "XaricdekiUnvanlar-Postalcode", "19801" },
                    { 21, "XaricdekiUnvanlar-Country", "United States" },
                    { 22, "XaricdekiUnvanlar-PhoneNumber", "862-5718476" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 22);
        }
    }
}
