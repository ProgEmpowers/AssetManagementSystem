using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthService.Migrations
{
    /// <inheritdoc />
    public partial class dffk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "75af95a9-9273-4c9b-86aa-0a80c76f32d6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "23424fae-bc18-4382-b26b-34868cf95cf6", "AQAAAAIAAYagAAAAEPq+WxokD9M6ZEuuDQFHMdwj3bNJAZzbR5zfy3d2SLZ+v73HqLKH13nnkQYIBRxJyg==", "096accae-5e38-4389-bc36-ac26c839feb9" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "75af95a9-9273-4c9b-86aa-0a80c76f32d6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5878e616-5433-4dab-bc7f-e335a5e97190", "AQAAAAIAAYagAAAAECxwBF+8DnfWEMATpETBA1so32t+BBEMBCggCYOj8Bp4z2tkbfDSpgtlVuQMLej44Q==", "44f0258d-c38f-425b-9581-f818d9850a08" });
        }
    }
}
