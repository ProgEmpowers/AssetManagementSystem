using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthService.Migrations
{
    /// <inheritdoc />
    public partial class t : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AssetAssignedTime",
                table: "UserAssets",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "75af95a9-9273-4c9b-86aa-0a80c76f32d6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "84e47378-1ee4-4a83-93c7-6c861027ebc8", "AQAAAAIAAYagAAAAEIIvcRwpGABYjY0V9zylFq+jFaD1SmuWdCBfULua6VW/CDZjQey4cqgR8kRGJaKXNw==", "bc694acf-b3f7-4191-9140-ee03f82acf97" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssetAssignedTime",
                table: "UserAssets");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "75af95a9-9273-4c9b-86aa-0a80c76f32d6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ef516c12-0a3e-466c-b68d-cbafe88d0aec", "AQAAAAIAAYagAAAAEMp72uSIEHS93NmDT9H0tQiyx/hnhpS8YHMBk3WIu9pj+NpJQm1d8tMGUKjJCRac/g==", "d3caf62f-761c-4974-a2c6-246ebe7dfe8c" });
        }
    }
}
