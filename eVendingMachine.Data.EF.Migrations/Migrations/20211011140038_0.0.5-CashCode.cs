using Microsoft.EntityFrameworkCore.Migrations;

namespace eVendingMachine.Data.EF.Migrations.Migrations
{
    public partial class _005CashCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Cashes",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Cashes");
        }
    }
}
