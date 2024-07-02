using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SupplyAssetType",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "IdOfVendor",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "Subject",
                table: "Contract");

            migrationBuilder.RenameColumn(
                name: "VendorName",
                table: "Contract",
                newName: "Optionals");

            migrationBuilder.RenameColumn(
                name: "SupplyAssetType",
                table: "Contract",
                newName: "NameOfVendors");

            migrationBuilder.AlterColumn<string>(
                name: "MobileNo",
                table: "Vendor",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "SupplyAssetTypes",
                table: "Vendor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IdOfVendors",
                table: "Contract",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "OrderedAssetType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderedAsset = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ContractId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderedAssetType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderedAssetType_Contract_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contract",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderedAssetType_ContractId",
                table: "OrderedAssetType",
                column: "ContractId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderedAssetType");

            migrationBuilder.DropColumn(
                name: "SupplyAssetTypes",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "IdOfVendors",
                table: "Contract");

            migrationBuilder.RenameColumn(
                name: "Optionals",
                table: "Contract",
                newName: "VendorName");

            migrationBuilder.RenameColumn(
                name: "NameOfVendors",
                table: "Contract",
                newName: "SupplyAssetType");

            migrationBuilder.AlterColumn<int>(
                name: "MobileNo",
                table: "Vendor",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "SupplyAssetType",
                table: "Vendor",
                type: "nvarchar(max)",
                nullable: true);

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
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "Contract",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
