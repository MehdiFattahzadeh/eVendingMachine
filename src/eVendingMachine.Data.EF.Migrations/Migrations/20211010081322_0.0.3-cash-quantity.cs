using Microsoft.EntityFrameworkCore.Migrations;

namespace eVendingMachine.Data.EF.Migrations.Migrations
{
    public partial class _003cashquantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Wallets",
                newName: "Number");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Cashes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Cashes");

            migrationBuilder.RenameColumn(
                name: "Number",
                table: "Wallets",
                newName: "Quantity");
        }
    }
}
