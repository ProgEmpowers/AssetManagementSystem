using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthService.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "75af95a9-9273-4c9b-86aa-0a80c76f32d6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e5ebd660-1fad-40f4-a996-0a8f1ac28d40", "AQAAAAIAAYagAAAAEOYGWPH7zbf2XPOosmXVXFR48NRKbA1kY8QAH+CdyTXw60IaIraes15X5/i94DrGjA==", "35481d5b-b11c-4659-acf8-bb1beea4869e" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserAssets_AspNetUsers_UserId",
                table: "UserAssets",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAssets_AspNetUsers_UserId",
                table: "UserAssets");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "75af95a9-9273-4c9b-86aa-0a80c76f32d6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "97ade02b-ba0e-456e-839b-3fee3e2245c0", "AQAAAAIAAYagAAAAEOwkp18rjegJOkfRjyDZLIaLUOI/FBbg6bkigsRJ/9WEgw8Z5F3Z94hDCEKkE9ghtQ==", "2244a4a9-0e9b-457d-8d40-f52886615213" });
        }
    }
}
