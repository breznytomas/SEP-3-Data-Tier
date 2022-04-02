using Microsoft.EntityFrameworkCore.Migrations;

namespace CarSharing_Database_GraphQL.Migrations
{
    public partial class _7_Add_Access_Level_Admin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Vehicles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "AccessLevel",
                table: "Customers",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "AccessLevel",
                table: "Customers");
        }
    }
}
