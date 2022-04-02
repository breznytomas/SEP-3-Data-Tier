using Microsoft.EntityFrameworkCore.Migrations;

namespace CarSharing_Database_GraphQL.Migrations
{
    public partial class _6_Add_Coupon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "Leases",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Coupons",
                columns: table => new
                {
                    Code = table.Column<string>(type: "text", nullable: false),
                    Discount = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupons", x => x.Code);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coupons");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Leases");
        }
    }
}
