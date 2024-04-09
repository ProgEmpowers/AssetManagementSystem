using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthService.Migrations
{
    /// <inheritdoc />
    public partial class azureauth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "75af95a9-9273-4c9b-86aa-0a80c76f32d6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b6514385-3113-43bf-97f7-3719e9f881b0", "AQAAAAIAAYagAAAAEFpmvLvAr455ZW+l1j/gu4G4TayIeFKTRQPPzBZoExwLkcgDF6PzHq8PGFNdoF3brA==", "0da4c45c-5535-43f1-8d1f-e97e9204ab83" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "75af95a9-9273-4c9b-86aa-0a80c76f32d6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c195237a-8f96-4045-8a04-c306d2d843b7", "AQAAAAIAAYagAAAAEBI5baR7KfA3WB3ZaeY/MwpL94q6SzZ4HMtA9d/z1LRQo1fMYxgw8dTOQ/AxPN1yhA==", "d820be9e-5b65-4218-a9fb-dd046f42501e" });
        }
    }
}
