using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthService.Migrations
{
    /// <inheritdoc />
    public partial class df : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "75af95a9-9273-4c9b-86aa-0a80c76f32d6",
                columns: new[] { "ConcurrencyStamp", "IsActive", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5878e616-5433-4dab-bc7f-e335a5e97190", true, "AQAAAAIAAYagAAAAECxwBF+8DnfWEMATpETBA1so32t+BBEMBCggCYOj8Bp4z2tkbfDSpgtlVuQMLej44Q==", "44f0258d-c38f-425b-9581-f818d9850a08" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "75af95a9-9273-4c9b-86aa-0a80c76f32d6",
                columns: new[] { "ConcurrencyStamp", "IsActive", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6e85a254-d300-465a-b2f3-ec3ba59e3c68", false, "AQAAAAIAAYagAAAAEJHNyFvbzfqaXkccroVnZ5BdxnzLZ5lIXpvcnZwZKkMUrFoHp0U/DOQ08Kg+q30dTQ==", "08f2d999-11bb-45e5-8936-90e624e2811f" });
        }
    }
}
