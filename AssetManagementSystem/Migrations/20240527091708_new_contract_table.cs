using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class new_contract_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Vendor",
                table: "Contract");

            migrationBuilder.AlterColumn<string>(
                name: "Subject",
                table: "Contract",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "Contract",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "SupplyAssetType",
                table: "Contract",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VendorName",
                table: "Contract",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SupplyAssetType",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "VendorName",
                table: "Contract");

            migrationBuilder.AlterColumn<string>(
                name: "Subject",
                table: "Contract",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "Contract",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Vendor",
                table: "Contract",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
