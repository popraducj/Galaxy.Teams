using Microsoft.EntityFrameworkCore.Migrations;

namespace Galaxy.Teams.Presentation.Migrations
{
    public partial class UpdateRobot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UnitsCoveredInADay",
                table: "Robots",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "TrustWorthyPercentage",
                table: "Robots",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "FuelConsumptionPerDay",
                table: "Robots",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "UnitsCoveredInADay",
                table: "Robots",
                type: "float",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<float>(
                name: "TrustWorthyPercentage",
                table: "Robots",
                type: "float",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<float>(
                name: "FuelConsumptionPerDay",
                table: "Robots",
                type: "float",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
