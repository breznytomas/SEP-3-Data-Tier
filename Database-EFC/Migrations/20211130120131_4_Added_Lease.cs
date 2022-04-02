using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CarSharing_Database_GraphQL.Migrations
{
    public partial class _4_Added_Lease : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Leases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LeasedFrom = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LeasedTo = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Canceled = table.Column<bool>(type: "boolean", nullable: false),
                    ListingId = table.Column<int>(type: "integer", nullable: true),
                    CustomerCpr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Leases_Customers_CustomerCpr",
                        column: x => x.CustomerCpr,
                        principalTable: "Customers",
                        principalColumn: "Cpr",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Leases_Listings_ListingId",
                        column: x => x.ListingId,
                        principalTable: "Listings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Leases_CustomerCpr",
                table: "Leases",
                column: "CustomerCpr");

            migrationBuilder.CreateIndex(
                name: "IX_Leases_ListingId",
                table: "Leases",
                column: "ListingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Leases");
        }
    }
}
