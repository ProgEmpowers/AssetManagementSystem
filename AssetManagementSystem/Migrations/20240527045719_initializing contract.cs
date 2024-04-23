using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class initializingcontract : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Contract",
                newName: "Vendor");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Contract",
                newName: "Subject");

            migrationBuilder.AlterColumn<string>(
                name: "SupplyAssetType",
                table: "Vendor",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "IdOfVendor",
                table: "Contract",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "Contract",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdOfVendor",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "Contract");

            migrationBuilder.RenameColumn(
                name: "Vendor",
                table: "Contract",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Subject",
                table: "Contract",
                newName: "Description");

            migrationBuilder.AlterColumn<string>(
                name: "SupplyAssetType",
                table: "Vendor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
