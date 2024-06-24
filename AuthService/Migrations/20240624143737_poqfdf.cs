using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthService.Migrations
{
    /// <inheritdoc />
    public partial class poqfdf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "75af95a9-9273-4c9b-86aa-0a80c76f32d6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e0e3439c-a986-4042-acab-493b8735132d", "AQAAAAIAAYagAAAAEF642c12wt36xlUcVZaoUEpWd0gz3X3+qZDR5cMZr8Jnhr3Y9G3B2al47jOZiWNyXg==", "00b1f600-e0d1-456e-a331-57b20a9f8348" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "75af95a9-9273-4c9b-86aa-0a80c76f32d6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "54d60cec-603e-4766-ae6a-d74a50bc9859", "AQAAAAIAAYagAAAAEDHqbQf8S8n+ybPSCLLUv+aARyovqCUrlZIQHBODxSsKH4sieQESYMnbB3AYW8Aqfw==", "1205fcfd-9155-4bbb-a921-3b78d61f6110" });
        }
    }
}
