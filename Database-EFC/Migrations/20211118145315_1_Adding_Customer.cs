using Microsoft.EntityFrameworkCore.Migrations;

namespace CarSharing_Database_GraphQL.Migrations
{
    public partial class _1_Adding_Customer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Cpr = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    LastName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    PhoneNo = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Cpr);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Username = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    CustomerCpr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Username);
                    table.ForeignKey(
                        name: "FK_Accounts_Customers_CustomerCpr",
                        column: x => x.CustomerCpr,
                        principalTable: "Customers",
                        principalColumn: "Cpr",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_OwnerCpr",
                table: "Vehicles",
                column: "OwnerCpr");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CustomerCpr",
                table: "Accounts",
                column: "CustomerCpr");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Customers_OwnerCpr",
                table: "Vehicles",
                column: "OwnerCpr",
                principalTable: "Customers",
                principalColumn: "Cpr",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Customers_OwnerCpr",
                table: "Vehicles");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_OwnerCpr",
                table: "Vehicles");
        }
    }
}
