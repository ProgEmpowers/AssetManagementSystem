using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthService.Migrations
{
    /// <inheritdoc />
    public partial class poq : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "75af95a9-9273-4c9b-86aa-0a80c76f32d6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "54d60cec-603e-4766-ae6a-d74a50bc9859", "AQAAAAIAAYagAAAAEDHqbQf8S8n+ybPSCLLUv+aARyovqCUrlZIQHBODxSsKH4sieQESYMnbB3AYW8Aqfw==", "1205fcfd-9155-4bbb-a921-3b78d61f6110" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "75af95a9-9273-4c9b-86aa-0a80c76f32d6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e97847e5-8429-4128-8f8c-c2683cfbf6af", "AQAAAAIAAYagAAAAEG/nQ24C+sEliDEMnPFBIU0pRu0ZcQNpWMK+9ZYjKA0HkP1whaXRd9RKTxevCixXWQ==", "0bed3761-a986-4c18-bfbe-dd927acdc8a6" });
        }
    }
}
