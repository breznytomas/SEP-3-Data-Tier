using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CarSharing_Database_GraphQL.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    LicenseNo = table.Column<string>(type: "text", nullable: false),
                    Brand = table.Column<string>(type: "text", nullable: true),
                    Model = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<string>(type: "text", nullable: true),
                    Transmission = table.Column<string>(type: "text", nullable: true),
                    FuelType = table.Column<string>(type: "text", nullable: true),
                    Seats = table.Column<int>(type: "integer", nullable: false),
                    ManufactureYear = table.Column<int>(type: "integer", nullable: false),
                    Mileage = table.Column<double>(type: "double precision", nullable: false),
                    OwnerCpr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.LicenseNo);
                });

            migrationBuilder.CreateTable(
                name: "Listings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ListedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Location = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    DateFrom = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DateTo = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    VehicleLicenseNo = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Listings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Listings_Vehicles_VehicleLicenseNo",
                        column: x => x.VehicleLicenseNo,
                        principalTable: "Vehicles",
                        principalColumn: "LicenseNo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Listings_VehicleLicenseNo",
                table: "Listings",
                column: "VehicleLicenseNo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Listings");

            migrationBuilder.DropTable(
                name: "Vehicles");
        }
    }
}
