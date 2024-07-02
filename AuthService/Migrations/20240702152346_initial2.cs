using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthService.Migrations
{
    /// <inheritdoc />
    public partial class initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "75af95a9-9273-4c9b-86aa-0a80c76f32d6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "27559298-1df7-446e-a362-056a8d84d74e", "AQAAAAIAAYagAAAAEAJMUK6lJJ4Lbwak+jX2ORiY2zqpLnaWeVX8muXQI/GbjjZKnxLGyJWoBTbLbt3ebg==", "24aebaed-af5b-4597-a5c5-8869445afb94" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "75af95a9-9273-4c9b-86aa-0a80c76f32d6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5f990943-6e9a-4784-bcb0-398d6e199788", "AQAAAAIAAYagAAAAEN6+YHPBwM8vKj5s6Xm3ba9o6vXdiIJ8WMfdMlCg5NR2uMoLWNoptw5YLLcV58+7ew==", "610967f7-02b8-4edd-85b6-5073ae1bdfb1" });
        }
    }
}
