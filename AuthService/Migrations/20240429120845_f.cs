using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthService.Migrations
{
    /// <inheritdoc />
    public partial class f : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "75af95a9-9273-4c9b-86aa-0a80c76f32d6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d018ccdb-fa82-42ff-9197-ffdb2eaada8b", "AQAAAAIAAYagAAAAEDKJNTUJQ4RKVCgor2jHXJzq582RMrNXa/MW8RktCg9XtDM51TTLwkQBMwLst2662Q==", "3802ba6b-fa47-4511-9340-38fff3b6ebc6" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "75af95a9-9273-4c9b-86aa-0a80c76f32d6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "008bbbd1-eb82-442d-9293-f259d920f831", "AQAAAAIAAYagAAAAEEjDIk8RoklmcFI9f7gNMhgVgJRK2OS8MKfsP36ZKNY9OSVRzR+EzVgznlgXIQYPPA==", "9e5db7b7-7d34-4f4c-bc91-7823ab87f359" });
        }
    }
}
