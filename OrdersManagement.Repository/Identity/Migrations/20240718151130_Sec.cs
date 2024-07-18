using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrdersManagement.Repository.Identity.Migrations
{
    public partial class Sec : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "b358fd21-5a55-45a7-a776-1198ae1813ce");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "62e21115-c0fa-42ef-8985-d78b7208e876", "maryamgm3323@gmail.com", "MARYAMGM3323@GMAIL.COM", "AQAAAAEAACcQAAAAEPegFoNtSZOt3a0c/ox1l+RXg/rtxP+7suse9CSbMZPUH13RNapnOs/SQ1qDOQctow==", "5dc7d410-3fb5-429a-ada5-9b3aabb9b6c9", "maryamgm3323@gmail.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "39a25e98-7479-4f1c-acd7-d1ec9ff3a7b3");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "3461ce46-a577-46d3-995c-1b6de1669859", "admin@example.com", "ADMIN@EXAMPLE.COM", "AQAAAAEAACcQAAAAEPcr8qR6V/xpsok8mk0t5c+38KhDOuTg1T4kLHQBm3QcQLdiHKVkXjavq60bkW2OIQ==", "7fa3389b-1f75-4a2b-9591-66892084b516", "admin@example.com" });
        }
    }
}
