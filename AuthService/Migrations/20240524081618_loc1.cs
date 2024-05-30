using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthService.Migrations
{
    /// <inheritdoc />
    public partial class loc1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "75af95a9-9273-4c9b-86aa-0a80c76f32d6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "784a12ad-caec-4588-b90d-74124cf334d6", "AQAAAAIAAYagAAAAEImKM38PVnKsRKbJYWUYHTTvv7Si+1RmUXffhGJci1WQa9+dNdnGSdJ43Rg2/i5z4g==", "28ae3a69-90d4-4426-aef1-e3a275263ef5" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "75af95a9-9273-4c9b-86aa-0a80c76f32d6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d018ccdb-fa82-42ff-9197-ffdb2eaada8b", "AQAAAAIAAYagAAAAEDKJNTUJQ4RKVCgor2jHXJzq582RMrNXa/MW8RktCg9XtDM51TTLwkQBMwLst2662Q==", "3802ba6b-fa47-4511-9340-38fff3b6ebc6" });
        }
    }
}
