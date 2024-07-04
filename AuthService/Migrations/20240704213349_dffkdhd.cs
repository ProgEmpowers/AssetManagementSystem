using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthService.Migrations
{
    /// <inheritdoc />
    public partial class dffkdhd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "75af95a9-9273-4c9b-86aa-0a80c76f32d6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "73a5b45a-83ce-441e-b9ee-74a76ce063e7", "AQAAAAIAAYagAAAAEC93BVu5PR74hP50dOdYf/gcc16p0mtDzCwmo63i+LUu9UrET2y89nF3OYZYrtOiTw==", "a3766eb0-f52b-43dd-b6dc-cb7cbdad40ea" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "75af95a9-9273-4c9b-86aa-0a80c76f32d6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "338e70f8-1c15-4533-aa4e-f8c4ce848db5", "AQAAAAIAAYagAAAAENAz9oEjXkc9L2RiB2e/BlMVv6UQ50MsuvrYnJHuhWkAIt6lpJWQeBB84zw9aANIFA==", "628ed1cd-1519-4b39-900f-214d0547851e" });
        }
    }
}
