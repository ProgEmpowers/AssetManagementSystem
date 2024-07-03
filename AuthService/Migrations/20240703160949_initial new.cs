using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthService.Migrations
{
    /// <inheritdoc />
    public partial class initialnew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "75af95a9-9273-4c9b-86aa-0a80c76f32d6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e94a32e4-a406-4230-816d-d5118be599d9", "AQAAAAIAAYagAAAAEIUIkz/n2OJsFHCO2e2P/yDBufKe1+qQYnrIHy6qWrRMfgi6bhPcZXpMIyL3cJMJ3w==", "2b13744e-8f07-46c2-955c-d1cb32fb2ce9" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "75af95a9-9273-4c9b-86aa-0a80c76f32d6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b02de218-8e69-4158-9d70-e9bb98a74c9b", "AQAAAAIAAYagAAAAEJ2s1smzhobh96LNIraIFPxylvwrTzjEv5EE3YDxzAoue+qWVnSjEyvShpr9c1wtrA==", "55044c85-7f95-4630-80ad-b26e1ad096bf" });
        }
    }
}
