using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthService.Migrations
{
    /// <inheritdoc />
    public partial class b4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "75af95a9-9273-4c9b-86aa-0a80c76f32d6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bb433607-ed76-4fb3-81c7-67098e7c69ea", "AQAAAAIAAYagAAAAEBefOOwftq2TRfLvhdTcPnPgLN+YkiZlgVdOWG8ddUHzuOTcEzrDN/HebpvxVIFn4w==", "33b4ec9c-e228-4e7b-b652-c2e2c476c617" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "75af95a9-9273-4c9b-86aa-0a80c76f32d6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5799d270-b990-4a5a-ac3d-28dd339ad79c", "AQAAAAIAAYagAAAAEFzDahZEB3E5ASiLFnsC08BgVso9jeCtA2DeZmK1/CPGnIwIjUV/bcOUkrWSwjJTPQ==", "b17e4013-2e83-4a20-8cdb-3d219296890b" });
        }
    }
}
