using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthService.Migrations
{
    /// <inheritdoc />
    public partial class _2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "75af95a9-9273-4c9b-86aa-0a80c76f32d6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "008bbbd1-eb82-442d-9293-f259d920f831", "AQAAAAIAAYagAAAAEEjDIk8RoklmcFI9f7gNMhgVgJRK2OS8MKfsP36ZKNY9OSVRzR+EzVgznlgXIQYPPA==", "9e5db7b7-7d34-4f4c-bc91-7823ab87f359" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "75af95a9-9273-4c9b-86aa-0a80c76f32d6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "24e024e0-0ec0-4a01-a060-3e66b597444e", "AQAAAAIAAYagAAAAEPi2nRfzBqpBbdt0p+b46qaO7/kNmO+EfO8XxJzacCjdR0n2wi3is1stxMnHbFIF2g==", "1a90e3c3-7679-4e74-84f4-88857b17d18a" });
        }
    }
}
