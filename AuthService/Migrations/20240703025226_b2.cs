using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthService.Migrations
{
    /// <inheritdoc />
    public partial class b2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "75af95a9-9273-4c9b-86aa-0a80c76f32d6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5799d270-b990-4a5a-ac3d-28dd339ad79c", "AQAAAAIAAYagAAAAEFzDahZEB3E5ASiLFnsC08BgVso9jeCtA2DeZmK1/CPGnIwIjUV/bcOUkrWSwjJTPQ==", "b17e4013-2e83-4a20-8cdb-3d219296890b" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "75af95a9-9273-4c9b-86aa-0a80c76f32d6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2ed2bd73-be6b-4ecc-a2c0-8932dfe039f0", "AQAAAAIAAYagAAAAEPmVs2LtMPh3D8SITaJMfihBO5jAKm3aNKdj/p5mU1rDNam1mbN1hn18USmjgC5arA==", "390ad4c6-43a6-48a2-a7f9-0be58d660cd5" });
        }
    }
}
