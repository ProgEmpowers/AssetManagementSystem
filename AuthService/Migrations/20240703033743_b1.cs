using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthService.Migrations
{
    /// <inheritdoc />
    public partial class b1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "75af95a9-9273-4c9b-86aa-0a80c76f32d6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b02de218-8e69-4158-9d70-e9bb98a74c9b", "AQAAAAIAAYagAAAAEJ2s1smzhobh96LNIraIFPxylvwrTzjEv5EE3YDxzAoue+qWVnSjEyvShpr9c1wtrA==", "55044c85-7f95-4630-80ad-b26e1ad096bf" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "75af95a9-9273-4c9b-86aa-0a80c76f32d6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e7d397b7-bcf0-4ea8-8d92-8b940e98b25e", "AQAAAAIAAYagAAAAEMwsFKAVEEi4yAhCspe0Ohl/ELbCgdtIVTv2YROzr3bTi5BQJuiloJkP5MxS/MoWcA==", "22069568-8ee8-401d-81bc-932034074914" });
        }
    }
}
