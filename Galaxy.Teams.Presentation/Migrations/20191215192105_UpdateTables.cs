using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Galaxy.Teams.Presentation.Migrations
{
    public partial class UpdateTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Captains",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Age = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    Expeditions = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Captains", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Robots",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(type: "varchar(256)", nullable: true),
                    Manufacturer = table.Column<string>(type: "varchar(256)", nullable: true),
                    Model = table.Column<string>(type: "varchar(256)", nullable: true),
                    Year = table.Column<int>(nullable: false),
                    UnitsCoveredInADay = table.Column<float>(nullable: false),
                    TrustWorthyPercentage = table.Column<float>(nullable: false),
                    FuelConsumptionPerDay = table.Column<float>(nullable: false),
                    NextRevision = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Robots", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shuttles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(type: "varchar(256)", nullable: true),
                    MaxSpeed = table.Column<int>(nullable: false),
                    Manufacturer = table.Column<string>(type: "varchar(256)", nullable: true),
                    Model = table.Column<string>(type: "varchar(256)", nullable: true),
                    Year = table.Column<int>(nullable: false),
                    FuelConsumption = table.Column<float>(nullable: false),
                    FuelTankLimit = table.Column<int>(nullable: false),
                    NextRevision = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shuttles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CaptainId = table.Column<Guid>(nullable: false),
                    ShuttleId = table.Column<Guid>(nullable: false),
                    Robots = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teams_Captains_CaptainId",
                        column: x => x.CaptainId,
                        principalTable: "Captains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Teams_Shuttles_ShuttleId",
                        column: x => x.ShuttleId,
                        principalTable: "Shuttles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Teams_CaptainId",
                table: "Teams",
                column: "CaptainId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_ShuttleId",
                table: "Teams",
                column: "ShuttleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Robots");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Captains");

            migrationBuilder.DropTable(
                name: "Shuttles");
        }
    }
}
