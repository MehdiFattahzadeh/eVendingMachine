using Microsoft.EntityFrameworkCore.Migrations;

namespace eVendingMachine.Data.EF.Migrations.Migrations
{
    public partial class _002removecashios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Number",
                table: "Wallets",
                newName: "Quantity");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Wallets",
                newName: "Number");
        }
    }
}
