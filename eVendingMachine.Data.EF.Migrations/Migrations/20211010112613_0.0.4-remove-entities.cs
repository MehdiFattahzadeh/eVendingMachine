using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eVendingMachine.Data.EF.Migrations.Migrations
{
    public partial class _004removeentities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CashIOs");

            migrationBuilder.DropTable(
                name: "Wallets");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CashIOs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CashId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InOut = table.Column<int>(type: "int", nullable: false),
                    PurchaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashIOs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CashIOs_Cashes_CashId",
                        column: x => x.CashId,
                        principalTable: "Cashes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CashIOs_Purchases_PurchaseId",
                        column: x => x.PurchaseId,
                        principalTable: "Purchases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wallets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CashId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wallets_Cashes_CashId",
                        column: x => x.CashId,
                        principalTable: "Cashes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CashIOs_CashId",
                table: "CashIOs",
                column: "CashId");

            migrationBuilder.CreateIndex(
                name: "IX_CashIOs_PurchaseId",
                table: "CashIOs",
                column: "PurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_CashId",
                table: "Wallets",
                column: "CashId");
        }
    }
}
